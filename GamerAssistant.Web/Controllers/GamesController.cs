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
            var bggGames = _sourceService.GetBggCollectionByUserId(userId);
            var gameCollection = _userService.GetGamesById(userId);

            //Get a string of the new game ids to send in an api call for game details
            var gameIds = GetNewBggGames(bggGames, gameCollection);

            //Get game details for all games in collection
            var gameDetails = _sourceService.GetGameDetailFromBggByGameId(gameIds);

            //Add the games to the database
            AddBggGames(id, gameDetails);
        }

        public string GetNewBggGames(BggGameCollection bggGames, IList<UserGame> gameCollection)
        {
            //Create an empty string array
            IList<string> gameIds = new List<string>();
            //Add each game's id to the list
            foreach (var game in bggGames.Items)
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
            //Convert the game id list to a comma delimited string
            string gameIdsCombined = string.Join(",", gameIds);

            return gameIdsCombined;
        }

        public void AddBggGames(int userId, BggGameDetail gameDetails)
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

        public JsonResult SearchBggGames(int userId, string query)
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
                var gameIds = GetNewBggGames(searchData, gameCollection);

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
                    var parentGameId = game.Links.Where(x => x.Type == "boardgameexpansion" && x.Inbound == "true").FirstOrDefault().Id;
                    if (parentGameId != null)
                        game.ParentGameId = parentGameId;
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

        public JsonResult GetGameCollection(int id)
        {
            //Initialize the model
            var model = new List<GameViewModel>();

            //Get the user's game collection
            var collection = _userService.GetGamesById(id);

            foreach (var item in collection)
            {
                //Get the game details
                var gameData = _gameService.GetGameById(item.GameId);

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
                    var gameCategories = _gameService.GetCategoriesByGameId(item.GameId);
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
                    var gameExpansions = collection.Where(x => x.ParentGameId == game.Id);
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
                    var gameGenres = _gameService.GetGenresByGameId(item.GameId);
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
                    var gameMechanics = _gameService.GetMechanicsByGameId(item.GameId);
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
                    var gamePlatforms = _gameService.GetPlatformsByGameId(item.GameId);
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
                    return Json(JsonResponseFactory.ErrorResponse(""), JsonRequestBehavior.AllowGet);
                }
            }

            return Json(JsonResponseFactory.SuccessResponse(model), JsonRequestBehavior.AllowGet);
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

    }


    //public void ImportBggCollection(int id)
    //{
    //    try
    //    {
    //        //Get the user id
    //        var userId = id;

    //        //Get the games in the database
    //        var gameDatabase = _gameService.GetGamesList();

    //        //Get the games in the user's BGG collection
    //        var bggGames = _sourceService.GetBggCollectionByUserId(userId);
    //        var gameCollection = _userService.GetGamesById(userId);

    //        //Create an empty string array
    //        IList<string> strings = new List<string>();
    //        //Add each game's id to the list
    //        foreach (var game in bggGames.Items)
    //        {
    //            //Check to see if the game exists in the user's collection
    //            var gameExistsInCollection = gameCollection.Any(x => x.GameId == game.ObjectId);

    //            if (!gameExistsInCollection)
    //            {
    //                //Add the game id to the string list
    //                string item = game.ObjectId.ToString();
    //                strings.Add(item);
    //            }
    //        };
    //        //Convert the game id list to a comma delimited string
    //        string commaDelimitedString = string.Join(",", strings);
    //        //Get game details for all games in collection
    //        var gameDetails = _sourceService.GetGameDetailFromBggByGameId(commaDelimitedString);

    //        foreach (var game in bggGames.Items)
    //        {
    //            //Check to see if the game exists in the database
    //            var gameExistsInDatabase = gameDatabase.Any(x => x.Id == game.ObjectId);
    //            //Check to see if the game exists in the user's collection
    //            var gameExistsInCollection = gameCollection.Any(x => x.GameId == game.ObjectId);
    //            var details = gameDetails.Items.FirstOrDefault(x => x.Id == game.ObjectId.ToString());

    //            if (details != null)
    //            {
    //                if (!gameExistsInCollection)
    //                {
    //                    if (!gameExistsInDatabase)
    //                    {
    //                        //Add the game to the game table
    //                        var gameToAdd = new Game()
    //                        {
    //                            Id = 0,
    //                            TabletopSourceType = (int)TabletopSourceType.BoardGameGeek,
    //                            TabletopSourceGameId = game.ObjectId,
    //                            GameType = game.SubType,
    //                            Name = game.Name,
    //                            Description = details.Description,
    //                            YearPublished = game.YearPublished,
    //                            MinPlayers = details.MinGamePlayers.Select(x => x.value).FirstOrDefault() ?? null,
    //                            MaxPlayers = details.MaxGamePlayers.Select(x => x.value).FirstOrDefault() ?? null,
    //                            PlayTime = null,
    //                            Image = game.Image,
    //                            ThumbnailImage = game.Thumbnail,
    //                            AddedOn = DateTime.Now
    //                        };
    //                        _gameService.AddGame(gameToAdd);

    //                        //Get a list of all expansion attached to game
    //                        var expansions = details.Links.Where(x => x.Type == "boardgameexpansion").ToList();

    //                        //foreach (var expansion in expansions)
    //                        //{
    //                        //    //Add each expansion mapping to the parent game
    //                        //    var expansionToAdd = new GameExpansion()
    //                        //    {
    //                        //        Id = 0,
    //                        //        GameId = game.ObjectId,
    //                        //        ExpansionGameId = int.Parse(expansion.Id)
    //                        //    };
    //                        //    _gameService.AddGameExpansion(expansionToAdd);
    //                        //}

    //                        //Get a list of categories associated with the game
    //                        var categories = details.Links.Where(x => x.Type == "boardgamecategory").ToList();
    //                        //Get a list of known categories
    //                        var gameCategories = _gameService.GetCategories();
    //                        foreach (var category in categories)
    //                        {
    //                            //Check to see if the category exists
    //                            var categoryExists = gameCategories.Any(x => x.Name == category.value);

    //                            int categoryId = -1;

    //                            if (!categoryExists)
    //                            {
    //                                //If it doesn't exist, add it to the main category table
    //                                var categoryToAdd = new Category()
    //                                {
    //                                    Id = 0,
    //                                    Name = category.value
    //                                };
    //                                var categoryAdded = _gameService.AddCategory(categoryToAdd);

    //                                categoryId = categoryAdded.Id;
    //                            }

    //                            if (categoryId == -1)
    //                                categoryId = _gameService.GetCategoryByName(category.value);

    //                            if (categoryId != -1)
    //                            {
    //                                //Add the category to the game
    //                                var gameCategoryToAdd = new GameCategory()
    //                                {
    //                                    Id = 0,
    //                                    GameId = game.ObjectId,
    //                                    CategoryId = categoryId,
    //                                    CategoryName = category.value
    //                                };
    //                                _gameService.AddGameCategory(gameCategoryToAdd);
    //                            }
    //                            else
    //                            {

    //                            }
    //                        }

    //                        //Get a list of categories associated with the game
    //                        var mechanics = details.Links.Where(x => x.Type == "boardgamemechanic").ToList();
    //                        //Get a list of known mechanics
    //                        var gameMechanics = _gameService.GetMechanics();
    //                        foreach (var mechanic in mechanics)
    //                        {
    //                            //Check to see if the mechanic exists
    //                            var mechanicExists = gameMechanics.Any(x => x.Name == mechanic.value);

    //                            int mechanicId = -1;

    //                            if (!mechanicExists)
    //                            {
    //                                //If it doesn't exist, add it to the main mechanic table
    //                                var mechanicToAdd = new Mechanic()
    //                                {
    //                                    Id = 0,
    //                                    Name = mechanic.value
    //                                };
    //                                var mechanicAdded = _gameService.AddMechanic(mechanicToAdd);

    //                                mechanicId = mechanicAdded.Id;
    //                            }

    //                            if (mechanicId == -1)
    //                                mechanicId = _gameService.GetMechanicByName(mechanic.value);

    //                            if (mechanicId != -1)
    //                            {
    //                                //Add the mechanic to the game
    //                                var gameMechanicToAdd = new GameMechanic()
    //                                {
    //                                    GameId = game.ObjectId,
    //                                    MechanicId = mechanicId,
    //                                    MechanicName = mechanic.value
    //                                };
    //                                _gameService.AddGameMechanic(gameMechanicToAdd);
    //                            }
    //                            else
    //                            {

    //                            }
    //                        }
    //                    }

    //                    //Add the game to the user
    //                    var userGame = new UserGame()
    //                    {
    //                        Id = 0,
    //                        UserId = userId,
    //                        GameId = game.ObjectId,
    //                        AddedOn = DateTime.Now
    //                    };
    //                    _userService.AddGameToUser(userGame);
    //                }
    //            }
    //            else
    //            {

    //            }
    //        };
    //    }
    //    catch
    //    {

    //    }
    //}

    //public JsonResult GetGameCollection(int id)
    //{
    //    var model = new List<GameViewModel>();

    //    var collection = _userService.GetGamesById(id);

    //    foreach (var item in collection)
    //    {
    //        var gameData = _gameService.GetGameById(item.GameId);

    //        if (gameData != null)
    //        {
    //            var minPlayers = gameData.MinPlayers.FirstOrDefault();
    //            var maxPlayers = gameData.MaxPlayers.FirstOrDefault();
    //            var yearPublished = gameData.YearPublished.FirstOrDefault();

    //            var game = new GameViewModel()
    //            {
    //                Id = gameData.TabletopSourceGameId,
    //                GameType = gameData.GameType,
    //                Name = gameData.Name,
    //                Description = gameData.Description,
    //                MinPlayers = minPlayers,
    //                MaxPlayers = maxPlayers,
    //                YearPublished = yearPublished,
    //                ImageUrl = gameData.Image,
    //                ThumbnailUrl = gameData.ThumbnailImage
    //            };

    //            var gameCategories = _gameService.GetCategoriesByGameId(item.GameId);
    //            if (gameCategories != null)
    //            {
    //                foreach (var gameCategory in gameCategories)
    //                {
    //                    var category = new GameViewModel.Category()
    //                    {
    //                        Id = gameCategory.CategoryId,
    //                        Name = gameCategory.CategoryName
    //                    };
    //                    game.Categories.Add(category);
    //                }
    //            }

    //            var gameMechanics = _gameService.GetMechanicsByGameId(item.GameId);
    //            if (gameMechanics != null)
    //            {
    //                foreach (var gameMechanic in gameMechanics)
    //                {
    //                    var mechanic = new GameViewModel.Mechanic()
    //                    {
    //                        Id = gameMechanic.MechanicId,
    //                        Name = gameMechanic.MechanicName
    //                    };
    //                    game.Mechanics.Add(mechanic);
    //                }
    //            }

    //            model.Add(game);
    //        }
    //        else
    //        {
    //            return Json(JsonResponseFactory.ErrorResponse(""), JsonRequestBehavior.AllowGet);
    //        }
    //    }

    //    return Json(JsonResponseFactory.SuccessResponse(model), JsonRequestBehavior.AllowGet);
    //}

    //public JsonResult SearchBggGames(int userId, string query)
    //{
    //    var model = new List<GameViewModel>();

    //    var searchData = _sourceService.SearchBggGames(query);
    //    var gameCollection = _userService.GetGamesById(userId);

    //    //Create an empty string array
    //    IList<string> strings = new List<string>();
    //    //Add each game's id to the list
    //    foreach (var game in searchData.Items)
    //    {
    //        //Check to see if the game exists in the user's collection
    //        var gameExistsInCollection = gameCollection.Any(x => x.GameId == game.Id);

    //        if (!gameExistsInCollection)
    //        {
    //            //Add the game id to the string list
    //            string item = game.Id.ToString();
    //            strings.Add(item);
    //        }
    //    };
    //    //Convert the game id list to a comma delimited string
    //    string commaDelimitedString = string.Join(",", strings);
    //    //Get game details for all games in collection
    //    var gameDetails = _sourceService.GetGameDetailFromBggByGameId(commaDelimitedString);


    //    foreach (var item in gameDetails.Items)
    //    {
    //        var gameType = item.Names.Select(x => x.Type).FirstOrDefault();
    //        var name = item.Names.Select(x => x.value).FirstOrDefault();
    //        var minPlayers = item.MinGamePlayers.Select(x => x.value).FirstOrDefault();
    //        var maxPlayers = item.MaxGamePlayers.Select(x => x.value).FirstOrDefault();
    //        var yearPublished = item.YearGamePublished.Select(x => x.value).FirstOrDefault();
    //        var game = new GameViewModel()
    //        {
    //            Id = Int32.Parse(item.Id),
    //            GameType = gameType,
    //            Name = name,
    //            Description = item.Description,
    //            MinPlayers = int.Parse(minPlayers),
    //            MaxPlayers = int.Parse(maxPlayers),
    //            ImageUrl = item.Image,
    //            ThumbnailUrl = item.Thumbnail,
    //            YearPublished = int.Parse(yearPublished)
    //        };

    //        var categories = item.Links.Where(x => x.Type == "boardgamecategory");

    //        if (categories != null)
    //        {
    //            foreach (var category in categories)
    //            {
    //                var gameCategory = new GameViewModel.Category()
    //                {
    //                    Id = int.Parse(category.Id),
    //                    Name = category.value
    //                };
    //                game.Categories.Add(gameCategory);
    //            }
    //        }

    //        var expansions = item.Links.Where(x => x.Type == "boardgameexpansion");

    //        if (expansions != null)
    //        {
    //            foreach (var expansion in expansions)
    //            {
    //                var gameExpansion = new GameViewModel.Expansion()
    //                {
    //                    Id = int.Parse(expansion.Id),
    //                    Name = expansion.value
    //                };
    //                game.Expansions.Add(gameExpansion);
    //            }
    //        }

    //        var mechanics = item.Links.Where(x => x.Type == "boardgamemechanic");

    //        if (mechanics != null)
    //        {
    //            foreach (var mechanic in mechanics)
    //            {
    //                var gameMechanic = new GameViewModel.Mechanic()
    //                {
    //                    Id = int.Parse(mechanic.Id),
    //                    Name = mechanic.value
    //                };
    //                game.Mechanics.Add(gameMechanic);
    //            }
    //        }

    //        model.Add(game);
    //    }

    //    return Json(JsonResponseFactory.SuccessResponse(model), JsonRequestBehavior.AllowGet);
    //}

    //public void AddGameToLibrary(int userId, GameViewModel model)
    //{
    //    var gameExistsInDatabase = _gameService.GetCategoriesByGameId(model.Id).Any(x => x.GameId == model.Id);
    //    var gameExistsInLibrary = _userService.GetGamesById(userId).Any(x => x.GameId == model.Id);

    //    if (!gameExistsInDatabase)
    //        AddGameToDatabase(model);

    //    if (!gameExistsInLibrary)
    //    {
    //        var userGame = new UserGame()
    //        {
    //            Id = 0,
    //            UserId = userId,
    //            GameId = model.Id,
    //            AddedOn = DateTime.Now
    //        };
    //        _userService.AddGameToUser(userGame);
    //    }
    //}

    //public void RemoveGameFromLibrary(int userId, int gameId)
    //{
    //    var userGame = _userService.GetGamesById(userId).FirstOrDefault(x => x.GameId == gameId);
    //    if (userGame != null)
    //    {
    //        //Delete the game from the user's collection
    //        _userService.DeleteGameFromUser(userGame.Id);

    //        //Determine if other players have this game
    //        var usersExist = _userService.GetUsersByGameId(gameId).Count(x => x.UserId != userId) > 0;
    //        //If not, delete it from the database
    //        if (usersExist)
    //        {
    //            RemoveGameFromDatabase(gameId);
    //        }
    //    }
    //}

    //public void AddGameToDatabase(GameViewModel model)
    //{
    //    var game = new Game()
    //    {
    //        Id = 0,
    //        TabletopSourceType = (int)TabletopSourceType.BoardGameGeek,
    //        TabletopSourceGameId = model.Id,
    //        GameType = model.GameType,
    //        Name = model.GameType,
    //        Description = model.Description,
    //        YearPublished = model.YearPublished.ToString(),
    //        MinPlayers = model.MinPlayers.ToString(),
    //        MaxPlayers = model.MaxPlayers.ToString(),
    //        Image = model.ImageUrl,
    //        ThumbnailImage = model.ThumbnailUrl,

    //        AddedOn = DateTime.Now
    //    };
    //    _gameService.AddGame(game);

    //    if (model.Categories != null)
    //    {
    //        var gameCategories = _gameService.GetCategories();

    //        foreach (var category in model.Categories)
    //        {
    //            //Determine if the category already exists in the database
    //            var categoryExists = gameCategories.Any(x => x.Name == category.Name);
    //            var categoryId = -1;
    //            if (categoryExists)
    //            {
    //                //Add the category to the database
    //                var categoryToAdd = new Category()
    //                {
    //                    Id = 0,
    //                    Name = category.Name
    //                };
    //                var categoryAdded = _gameService.AddCategory(categoryToAdd);
    //                categoryId = categoryAdded.Id;
    //            }

    //            if (categoryId == -1)
    //                categoryId = _gameService.GetCategoryByName(category.Name);

    //            if (categoryId != -1)
    //            {
    //                //Add the category to the game
    //                var gameCategory = new GameCategory()
    //                {
    //                    Id = 0,
    //                    GameId = game.Id,
    //                    CategoryId = categoryId,
    //                    CategoryName = category.Name
    //                };
    //                _gameService.AddGameCategory(gameCategory);
    //            }
    //        }
    //    }
    //    else
    //    {

    //    }

    //    if (model.Mechanics != null)
    //    {
    //        var gameMechanics = _gameService.GetMechanics();

    //        foreach (var mechanic in model.Mechanics)
    //        {
    //            //Determine if the mechanic already exists in the database
    //            var mechanicExists = gameMechanics.Any(x => x.Name == mechanic.Name);
    //            var mechanicId = -1;
    //            if (mechanicExists)
    //            {
    //                //Add the mechanic to the database
    //                var mechanicToAdd = new Mechanic()
    //                {
    //                    Id = 0,
    //                    Name = mechanic.Name
    //                };
    //                var mechanicAdded = _gameService.AddMechanic(mechanicToAdd);
    //                mechanicId = mechanicAdded.Id;
    //            }

    //            if (mechanicId == -1)
    //                mechanicId = _gameService.GetMechanicByName(mechanic.Name);

    //            if (mechanicId != -1)
    //            {
    //                //Add the mechanic to the game
    //                var gameMechanic = new GameMechanic()
    //                {
    //                    Id = 0,
    //                    GameId = game.Id,
    //                    MechanicId = mechanicId,
    //                    MechanicName = mechanic.Name
    //                };
    //                _gameService.AddGameMechanic(gameMechanic);
    //            }
    //        }
    //    }
    //    else
    //    {

    //    }
    //}

    //public void RemoveGameFromDatabase(int gameId)
    //{
    //    var game = _gameService.GetGameById(gameId);

    //    if (game != null)
    //    {
    //        var gameCategories = _gameService.GetCategoriesByGameId(gameId);
    //        if (gameCategories != null)
    //        {
    //            foreach (var gameCategory in gameCategories)
    //            {
    //                _gameService.DeleteGameCategoryById(gameCategory.Id);
    //            }
    //        }

    //        //var gameExpansions = _gameService.GetExpansionsByGameId(gameId);
    //        //if (gameExpansions != null)
    //        //{
    //        //    foreach (var gameExpansion in gameExpansions)
    //        //    {
    //        //        _gameService.DeleteGameExpansionById(gameExpansion.Id);
    //        //    }
    //        //}

    //        var gameMechanics = _gameService.GetMechanicsByGameId(gameId);
    //        if (gameMechanics != null)
    //        {
    //            foreach (var gameMechanic in gameMechanics)
    //            {
    //                _gameService.DeleteGameMechanicById(gameMechanic.Id);
    //            }
    //        }

    //        _gameService.DeleteGameById(gameId);

    //    }
    //    _gameService.DeleteGameById(gameId);
    //}
}
