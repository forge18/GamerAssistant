using GamerAssistant.Games;
using GamerAssistant.Sources;
using GamerAssistant.Users;
using GamerAssistant.Web.Infrastructure;
using GamerAssistant.Web.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GamerAssistant.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameAppService _gameService;
        private readonly SourceAppService _sourceService;
        private readonly UserAppService _userService;

        public GamesController(
            GameAppService gameService,
            SourceAppService sourceService,
            UserAppService userService
        )
        {
            _gameService = gameService;
            _sourceService = sourceService;
            _userService = userService;
        }


        // GET: GameLibrary
        public ActionResult Index()
        {
            return View();
        }

        public void ImportBggCollection(int id)
        {
            //Get the user id
            var userId = id;

            //Get the games in the bgg database
            var bggCollection = _sourceService.GetBggCollectionByUserId(userId);
            var gameCollection = _userService.GetGamesById(userId);

            //Get a string of the new game ids to send in an api call for game details
            var gameIds = IdentifyNewBggGames(bggCollection, new BggGameSearch(), gameCollection);

            //Get game details for all games in collection
            var gameDetails = _sourceService.GetGameDetailFromBggByGameId(gameIds);

            //Add the games to the database
            AddGames(id, gameDetails);
        }

        public JsonResult GetGamesLists(int userId)
        {
            try
            {
                var games = GetCollection(userId, "collection");
                var favoriteGames = GetCollection(userId, "favorites");
                var friendGames = GetFriendCollections(userId);

                if (games != null)
                {
                    var model = new GameListsViewModel()
                    {
                        UserId = userId,
                        FirstName = "",
                        LastName = "",
                        BggGames = games,
                        FavoriteGames = favoriteGames,
                        FriendsGames = friendGames
                    };
                    return Json(JsonResponseFactory.SuccessResponse(model), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(JsonResponseFactory.ErrorResponse(""), JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(JsonResponseFactory.ErrorResponse(""), JsonRequestBehavior.AllowGet);
            }
        }

        public List<GameViewModel> GetCollection(int id, string type)
        {
            //Initialize the model
            var model = new List<GameViewModel>();
            var collection = new List<int>();

            if (type == "collection")
            {
                //Get the user's game collection
                var games = _userService.GetGamesById(id);

                foreach (var game in games)
                {
                    collection.Add(game.GameId);
                }
            }
            else if (type == "favorites")
            {
                var favorites = _userService.GetFavoritesById(id);

                foreach (var favorite in favorites)
                {
                    collection.Add(favorite.GameId);
                }
            }


            foreach (var item in collection)
            {
                //Get the game details
                var gameData = _gameService.GetGameById(item);

                if (gameData != null)
                {
                    //Prep the data points
                    var minPlayers = gameData.MinPlayers.FirstOrDefault();
                    var maxPlayers = gameData.MaxPlayers.FirstOrDefault();
                    var yearPublished = gameData.YearPublished.FirstOrDefault();

                    //Create the model
                    var game = new GameViewModel()
                    {
                        Id = gameData.TabletopSourceGameId,
                        GameType = gameData.GameType,
                        Name = gameData.Name,
                        Description = gameData.Description,
                        MinPlayers = minPlayers,
                        MaxPlayers = maxPlayers,
                        YearPublished = yearPublished,
                        ImageUrl = gameData.Image,
                        IsExpansion = gameData.GameType == "BoardGameExpansion",
                        ThumbnailUrl = gameData.ThumbnailImage
                    };

                    //Add categories to the model
                    var gameCategories = _gameService.GetCategoriesByGameId(item);
                    if (gameCategories != null)
                    {
                        foreach (var gameCategory in gameCategories)
                        {
                            var category = new GameViewModel.Category()
                            {
                                Id = gameCategory.CategoryId,
                                Name = gameCategory.CategoryName
                            };
                            game.Categories.Add(category);
                        }
                    }

                    //Add expansions to the model
                    var gameExpansions = _gameService.GetGamesList().Where(x => x.ParentGameId == game.Id);
                    if (gameExpansions != null)
                    {
                        foreach (var gameExpansion in gameExpansions)
                        {
                            var expansion = new GameViewModel.Expansion()
                            {
                                Id = gameExpansion.TabletopSourceGameId,
                                Name = gameExpansion.Name
                            };
                            game.Expansions.Add(expansion);
                        }
                    }

                    //Add genres to the model
                    var gameGenres = _gameService.GetGenresByGameId(item);
                    if (gameGenres != null)
                    {
                        foreach (var gameGenre in gameGenres)
                        {
                            var genre = new GameViewModel.Genre()
                            {
                                Id = gameGenre.GenreId,
                                Name = gameGenre.GenreName
                            };
                            game.Genres.Add(genre);
                        }
                    }

                    //Add mechanics to the model
                    var gameMechanics = _gameService.GetMechanicsByGameId(item);
                    if (gameMechanics != null)
                    {
                        foreach (var gameMechanic in gameMechanics)
                        {
                            var mechanic = new GameViewModel.Mechanic()
                            {
                                Id = gameMechanic.MechanicId,
                                Name = gameMechanic.MechanicName
                            };
                            game.Mechanics.Add(mechanic);
                        }
                    }

                    //Add platforms to the model
                    var gamePlatforms = _gameService.GetPlatformsByGameId(item);
                    if (gamePlatforms != null)
                    {
                        foreach (var gamePlatform in gamePlatforms)
                        {
                            var platform = new GameViewModel.Platform()
                            {
                                Id = gamePlatform.PlatformId,
                                Name = gamePlatform.PlatformName
                            };
                            game.Platforms.Add(platform);
                        }
                    }

                    //Add game to the model
                    model.Add(game);
                }
                else
                {

                }
            }

            return model;
        }

        public List<GameViewModel> GetFriendCollections(int id)
        {
            var combinedFriendGames = new List<GameViewModel>();

            var friends = _userService.GetFriendsById(id);
            foreach (var friend in friends)
            {
                var friendGames = GetCollection(friend.UserId, "collection");
                foreach (var game in friendGames)
                {
                    if (!combinedFriendGames.Contains(game))
                    {
                        combinedFriendGames.Add(game);
                    }
                }

            }

            return combinedFriendGames;
        }

        public JsonResult SearchGames(int userId, string query)
        {
            //Initialize the model
            var model = new List<GameViewModel>();

            //Get the search data from bgg
            var searchData = _sourceService.SearchBggGames(query);
            //Get the user's collection data
            var gameCollection = _userService.GetGamesById(userId);

            if (searchData != null && gameCollection != null)
            {
                //Determine which games in the search results are not in the user's collection
                var gameIds = IdentifyNewBggGames(new BggGameCollection(), searchData, gameCollection);

                if (gameIds != null)
                {
                    //Get game details for all new games
                    var gameDetails = _sourceService.GetGameDetailFromBggByGameId(gameIds);
                    model = FormatBggGameData(gameDetails);

                    if (model != null)
                        return Json(JsonResponseFactory.SuccessResponse(model), JsonRequestBehavior.AllowGet);
                }
            }

            return Json(JsonResponseFactory.ErrorResponse(""), JsonRequestBehavior.AllowGet);
        }

        public void AddGames(int userId, BggGameDetail gameDetails)
        {
            //Get the games in the app database
            var gameDatabase = _gameService.GetGamesList();

            foreach (var game in gameDetails.Items)
            {
                //Check to see if the game exists in the database
                var gameExistsInDatabase = gameDatabase.Any(x => x.Id == int.Parse(game.Id));
                var gameType = GetGameType(game.Type);

                if (game != null && gameType != "Invalid")
                {
                    if (!gameExistsInDatabase)
                    {
                        /*************************************************************/
                        /*************************Add Game****************************/
                        //Create model to add game to the game table
                        var gameToAdd = new Game()
                        {
                            Id = 0,
                            TabletopSourceType = (int)TabletopSourceType.BoardGameGeek,
                            TabletopSourceGameId = int.Parse(game.Id),
                            GameType = gameType,
                            Name = game.Names.Select(x => x.value).FirstOrDefault() ?? null,
                            Description = game.Description,
                            YearPublished = game.YearGamePublished.Select(x => x.value).FirstOrDefault() ?? null,
                            MinPlayers = game.MinGamePlayers.Select(x => x.value).FirstOrDefault() ?? null,
                            MaxPlayers = game.MaxGamePlayers.Select(x => x.value).FirstOrDefault() ?? null,
                            PlayTime = null,
                            Image = game.Image,
                            ThumbnailImage = game.Thumbnail,
                            IsExpansion = gameType == "BoardGameExpansion",
                            AddedOn = DateTime.Now
                        };

                        //If the game is an expansion, find the parent game id
                        if (gameToAdd.IsExpansion)
                        {
                            var parentGameId = game.Links.Where(x => x.Type == "boardgameexpansion" && x.Inbound == "true").FirstOrDefault().Id;
                            if (parentGameId != null)
                                gameToAdd.ParentGameId = int.Parse(parentGameId);
                        }

                        //Add the game to the database
                        _gameService.AddGame(gameToAdd);

                        /*******************************************************************/
                        /*************************Add Categories****************************/
                        //Get a list of categories associated with the game
                        var categories = game.Links.Where(x => x.Type == "boardgamecategory").ToList();
                        if (categories != null)
                        {
                            //Get a list of known categories
                            var gameCategories = _gameService.GetCategories();

                            foreach (var category in categories)
                            {
                                //Check to see if the category exists
                                var categoryExists = gameCategories.Any(x => x.Name == category.value);

                                int categoryId = -1;

                                if (!categoryExists)
                                {
                                    //If it doesn't exist, add it to the main category table
                                    var categoryToAdd = new Category()
                                    {
                                        Id = 0,
                                        Name = category.value
                                    };
                                    var categoryAdded = _gameService.AddCategory(categoryToAdd);

                                    categoryId = categoryAdded.Id;
                                }

                                if (categoryId == -1)
                                    categoryId = _gameService.GetCategoryByName(category.value);

                                if (categoryId != -1)
                                {
                                    //Add the category to the game
                                    var gameCategoryToAdd = new GameCategory()
                                    {
                                        Id = 0,
                                        GameId = int.Parse(game.Id),
                                        CategoryId = categoryId,
                                        CategoryName = category.value
                                    };
                                    _gameService.AddGameCategory(gameCategoryToAdd);
                                }
                            }
                        }

                        /*****************************************************************/
                        /***************************Add Genres****************************/
                        //Get a list of genres associated with the game
                        var genres = game.Links.Where(x => x.Type == "videogamegenre").ToList();
                        if (genres != null)
                        {
                            //Get a list of known genres
                            var gameGenres = _gameService.GetGenres();

                            foreach (var genre in genres)
                            {
                                //Check to see if the genre exists
                                var genreExists = gameGenres.Any(x => x.Name == genre.value);

                                int genreId = -1;

                                if (!genreExists)
                                {
                                    //If it doesn't exist, add it to the main genre table
                                    var genreToAdd = new Genre()
                                    {
                                        Id = 0,
                                        Name = genre.value
                                    };
                                    var genreAdded = _gameService.AddGenre(genreToAdd);

                                    genreId = genreAdded.Id;
                                }

                                if (genreId == -1)
                                    genreId = _gameService.GetGenreByName(genre.value);

                                if (genreId != -1)
                                {
                                    //Add the genre to the game
                                    var gameGenreToAdd = new GameGenre()
                                    {
                                        Id = 0,
                                        GameId = int.Parse(game.Id),
                                        GenreId = genreId,
                                        GenreName = genre.value
                                    };
                                    _gameService.AddGameGenre(gameGenreToAdd);
                                }
                            }
                        }



                        /*******************************************************************/
                        /*************************Add Mechanics****************************/
                        //Get a list of categories associated with the game
                        var mechanics = game.Links.Where(x => x.Type == "boardgamemechanic").ToList();
                        if (mechanics != null)
                        {
                            //Get a list of known mechanics
                            var gameMechanics = _gameService.GetMechanics();

                            foreach (var mechanic in mechanics)
                            {
                                //Check to see if the mechanic exists
                                var mechanicExists = gameMechanics.Any(x => x.Name == mechanic.value);

                                int mechanicId = -1;

                                if (!mechanicExists)
                                {
                                    //If it doesn't exist, add it to the main mechanic table
                                    var mechanicToAdd = new Mechanic()
                                    {
                                        Id = 0,
                                        Name = mechanic.value
                                    };
                                    var mechanicAdded = _gameService.AddMechanic(mechanicToAdd);

                                    mechanicId = mechanicAdded.Id;
                                }

                                if (mechanicId == -1)
                                    mechanicId = _gameService.GetMechanicByName(mechanic.value);

                                if (mechanicId != -1)
                                {
                                    //Add the mechanic to the game
                                    var gameMechanicToAdd = new GameMechanic()
                                    {
                                        GameId = int.Parse(game.Id),
                                        MechanicId = mechanicId,
                                        MechanicName = mechanic.value
                                    };
                                    _gameService.AddGameMechanic(gameMechanicToAdd);
                                }
                                else
                                {

                                }
                            }
                        }

                        /*******************************************************************/
                        /*************************Add Platforms****************************/
                        //Get a list of categories associated with the game
                        var platforms = game.Links.Where(x => x.Type == "videogameplatform").ToList();
                        if (platforms != null)
                        {
                            //Get a list of known platforms
                            var gamePlatforms = _gameService.GetPlatforms();

                            foreach (var platform in platforms)
                            {
                                //Check to see if the platform exists
                                var platformExists = gamePlatforms.Any(x => x.Name == platform.value);

                                int platformId = -1;

                                if (!platformExists)
                                {
                                    //If it doesn't exist, add it to the main platform table
                                    var platformToAdd = new Platform()
                                    {
                                        Id = 0,
                                        Name = platform.value
                                    };
                                    var platformAdded = _gameService.AddPlatform(platformToAdd);

                                    platformId = platformAdded.Id;
                                }

                                if (platformId == -1)
                                    platformId = _gameService.GetPlatformByName(platform.value);

                                if (platformId != -1)
                                {
                                    //Add the platform to the game
                                    var gamePlatformToAdd = new GamePlatform()
                                    {
                                        Id = 0,
                                        GameId = int.Parse(game.Id),
                                        PlatformId = platformId,
                                        PlatformName = platform.value
                                    };
                                    _gameService.AddGamePlatform(gamePlatformToAdd);
                                }
                            }
                        }

                        //Add the game to the user
                        var userGame = new UserGame()
                        {
                            Id = 0,
                            UserId = userId,
                            GameId = int.Parse(game.Id),
                            AddedOn = DateTime.Now
                        };
                        _userService.AddGameToUser(userGame);
                    }
                }
                else
                {
                    //Throw error
                }


            }
        }

        public void RemoveGameFromLibrary(int userId, int gameId)
        {
            var userGame = _userService.GetGamesById(userId).FirstOrDefault(x => x.GameId == gameId);
            if (userGame != null)
            {
                //Delete the game from the user's collection
                _userService.DeleteGameFromUser(userGame.Id);

                //Determine if other players have this game
                var usersExist = _userService.GetUsersByGameId(gameId).Count(x => x.UserId != userId) > 0;
                //If not, delete it from the database
                if (usersExist)
                {
                    RemoveGameFromDatabase(gameId);
                }
            }
        }

        public void RemoveGameFromDatabase(int gameId)
        {
            //Get the game
            var game = _gameService.GetGameById(gameId);

            if (game != null)
            {
                //Remove the categories associated with the game
                var gameCategories = _gameService.GetCategoriesByGameId(gameId);
                if (gameCategories != null)
                {
                    foreach (var gameCategory in gameCategories)
                    {
                        _gameService.DeleteGameCategoryById(gameCategory.Id);
                    }
                }

                //Remove the genres associated with the game
                var gameGenres = _gameService.GetGenresByGameId(gameId);
                if (gameGenres != null)
                {
                    foreach (var gameGenre in gameGenres)
                    {
                        _gameService.DeleteGameGenreById(gameGenre.Id);
                    }
                }

                //Remove the mechanics associated with the game
                var gameMechanics = _gameService.GetMechanicsByGameId(gameId);
                if (gameMechanics != null)
                {
                    foreach (var gameMechanic in gameMechanics)
                    {
                        _gameService.DeleteGameMechanicById(gameMechanic.Id);
                    }
                }

                //Remove the platforms associated with the game
                var gamePlatforms = _gameService.GetPlatformsByGameId(gameId);
                if (gamePlatforms != null)
                {
                    foreach (var gamePlatform in gamePlatforms)
                    {
                        _gameService.DeleteGamePlatformById(gamePlatform.Id);
                    }
                }

                _gameService.DeleteGameById(gameId);

            }
            _gameService.DeleteGameById(gameId);
        }

        public void AddFavoriteGame(int userId, int gameId)
        {
            var favoriteGame = new UserFavorite()
            {
                Id = 0,
                UserId = userId,
                GameId = gameId
            };

            _userService.AddFavorite(favoriteGame);
        }

        public void RemoveFavoriteGame(int userId, int gameId)
        {
            var favoriteGame = _userService.GetFavoritesById(userId).FirstOrDefault(x => x.GameId == gameId);

            if (favoriteGame != null)
                _userService.DeleteFavoriteById(favoriteGame.Id);
        }

        public List<GameViewModel> FormatBggGameData(BggGameDetail games)
        {
            //Initialize the model
            var model = new List<GameViewModel>();

            //Set the gamedetails variable with dataset
            var gameDetails = games;

            foreach (var item in gameDetails.Items)
            {
                //Prep data points
                var gameType = GetGameType(item.Type);
                var name = item.Names.Select(x => x.value).FirstOrDefault();
                var minPlayers = item.MinGamePlayers.Select(x => x.value).FirstOrDefault();
                var maxPlayers = item.MaxGamePlayers.Select(x => x.value).FirstOrDefault();
                var yearPublished = item.YearGamePublished.Select(x => x.value).FirstOrDefault();

                //Create the model
                var game = new GameViewModel()
                {
                    Id = Int32.Parse(item.Id),
                    GameType = gameType,
                    Name = name,
                    Description = item.Description,
                    MinPlayers = int.Parse(minPlayers),
                    MaxPlayers = int.Parse(maxPlayers),
                    ImageUrl = item.Image,
                    ThumbnailUrl = item.Thumbnail,
                    IsExpansion = gameType == "BoardGameExpansion",
                    YearPublished = int.Parse(yearPublished)
                };

                //If the game is an expansion, find the parent game id
                if (game.IsExpansion)
                {
                    var parentGameId = item.Links.Where(x => x.Type == "boardgameexpansion" && x.Inbound == "true").FirstOrDefault().Id;
                    if (parentGameId != null)
                        game.ParentGameId = int.Parse(parentGameId);
                }

                //Determine if categories are associated with the game
                var categories = item.Links.Where(x => x.Type == "boardgamecategory");

                //If there are categories, add them to the game
                if (categories != null)
                {
                    foreach (var category in categories)
                    {
                        var gameCategory = new GameViewModel.Category()
                        {
                            Id = int.Parse(category.Id),
                            Name = category.value
                        };
                        game.Categories.Add(gameCategory);
                    }
                }

                //Determine if genres are associated with the game
                var genres = item.Links.Where(x => x.Type == "videogamegenre");

                //If there are genres, add them to the game
                if (genres != null)
                {
                    foreach (var genre in genres)
                    {
                        var gameGenre = new GameViewModel.Genre()
                        {
                            Id = int.Parse(genre.Id),
                            Name = genre.value
                        };
                        game.Genres.Add(gameGenre);
                    }
                }

                //Determine if mechanics are associated with the game
                var mechanics = item.Links.Where(x => x.Type == "boardgamemechanic");

                //If there are mechanics, add them to the game
                if (mechanics != null)
                {
                    foreach (var mechanic in mechanics)
                    {
                        var gameMechanic = new GameViewModel.Mechanic()
                        {
                            Id = int.Parse(mechanic.Id),
                            Name = mechanic.value
                        };
                        game.Mechanics.Add(gameMechanic);
                    }
                }

                //Determine if platforms are associated with the game
                var platforms = item.Links.Where(x => x.Type == "videogameplatform");

                //If there are platforms, add them to the game
                if (platforms != null)
                {
                    foreach (var platform in platforms)
                    {
                        var gamePlatform = new GameViewModel.Platform()
                        {
                            Id = int.Parse(platform.Id),
                            Name = platform.value
                        };
                        game.Platforms.Add(gamePlatform);
                    }
                }

                model.Add(game);
            }

            return model;
        }

        public string IdentifyNewBggGames(BggGameCollection bggCollection, BggGameSearch bggSearch, IList<UserGame> gameCollection)
        {
            //Create an empty string array
            IList<string> gameIds = new List<string>();
            //Add each game's id to the list
            foreach (var game in bggCollection.Items)
            {
                //Check to see if the game exists in the user's collection
                var gameExistsInCollection = gameCollection.Any(x => x.GameId == game.ObjectId);

                if (!gameExistsInCollection)
                {
                    //Add the game id to the string list
                    string item = game.ObjectId.ToString();
                    gameIds.Add(item);
                }
            };
            //Add each game's id to the list
            foreach (var game in bggSearch.Items)
            {
                //Check to see if the game exists in the user's collection
                var gameExistsInSearch = gameCollection.Any(x => x.GameId == game.Id);

                if (!gameExistsInSearch)
                {
                    //Add the game id to the string list
                    string item = game.Id.ToString();
                    gameIds.Add(item);
                }
            };
            //Convert the game id list to a comma delimited string
            string gameIdsCombined = string.Join(",", gameIds);

            return gameIdsCombined;
        }

        public string GetGameType(string gameType)
        {
            var type = "Invalid";
            switch (gameType)
            {
                case "boardgame":
                    type = "BoardGame";
                    break;
                case "boardgameexpansion":
                    type = "BoardGameExpansion";
                    break;
                case "videogame":
                    type = "VideoGame";
                    break;
                case "rpgitem":
                    type = "Rpg";
                    break;
            };

            return type;
        }
    }
}


