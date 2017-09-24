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
    public class GameLibraryController : Controller
    {
        private readonly GameAppService _gameService;
        private readonly SourceAppService _sourceService;
        private readonly UserAppService _userService;

        public GameLibraryController(
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

        public void Test()
        {
            var userId = 3;
            var library = _sourceService.GetBggCollectionByUserId(userId);
            var gameDetails = _sourceService.GetGameDetailFromBggByGameId("215");
            var searchResults = _sourceService.SearchBggGames("Ark");
            //var test = 0;
        }

        public void ImportBggCollection(int id)
        {
            try
            {
                //Get the user id
                var userId = id;

                //Get the games in the database
                var gameDatabase = _gameService.GetTabletopGamesList();

                //Get the games in the user's BGG collection
                var bggGames = _sourceService.GetBggCollectionByUserId(userId);
                var gameCollection = _userService.GetGamesById(userId);

                //Create an empty string array
                IList<string> strings = new List<string>();
                //Add each game's id to the list
                foreach (var game in bggGames.Items)
                {
                    //Check to see if the game exists in the user's collection
                    var gameExistsInCollection = gameCollection.Any(x => x.GameId == game.ObjectId);

                    if (!gameExistsInCollection)
                    {
                        //Add the game id to the string list
                        string item = game.ObjectId.ToString();
                        strings.Add(item);
                    }
                };
                //Convert the game id list to a comma delimited string
                string commaDelimitedString = string.Join(",", strings);
                //Get game details for all games in collection
                var gameDetails = _sourceService.GetGameDetailFromBggByGameId(commaDelimitedString);

                foreach (var game in bggGames.Items)
                {
                    //Check to see if the game exists in the database
                    var gameExistsInDatabase = gameDatabase.Any(x => x.Id == game.ObjectId);
                    //Check to see if the game exists in the user's collection
                    var gameExistsInCollection = gameCollection.Any(x => x.GameId == game.ObjectId);
                    var details = gameDetails.Items.FirstOrDefault(x => x.Id == game.ObjectId.ToString());

                    if (details != null)
                    {
                        if (!gameExistsInCollection)
                        {
                            if (!gameExistsInDatabase)
                            {
                                //Add the game to the game table
                                var gameToAdd = new TabletopGame()
                                {
                                    Id = 0,
                                    TabletopSourceType = (int)TabletopSourceType.BoardGameGeek,
                                    TabletopSourceGameId = game.ObjectId,
                                    GameType = game.SubType,
                                    Name = game.Name,
                                    Description = details.Description,
                                    YearPublished = game.YearPublished,
                                    MinPlayers = details.MinGamePlayers.Select(x => x.value).FirstOrDefault() ?? null,
                                    MaxPlayers = details.MaxGamePlayers.Select(x => x.value).FirstOrDefault() ?? null,
                                    PlayTime = null,
                                    Image = game.Image,
                                    ThumbnailImage = game.Thumbnail,
                                    AddedOn = DateTime.Now
                                };
                                _gameService.AddTabletopGame(gameToAdd);

                                //Get a list of all expansion attached to game
                                var expansions = details.Links.Where(x => x.Type == "boardgameexpansion").ToList();

                                foreach (var expansion in expansions)
                                {
                                    //Add each expansion mapping to the parent game
                                    var expansionToAdd = new TabletopGameExpansion()
                                    {
                                        Id = 0,
                                        GameId = game.ObjectId,
                                        ExpansionGameId = int.Parse(expansion.Id)
                                    };
                                    _gameService.AddTabletopGameExpansion(expansionToAdd);
                                }

                                //Get a list of categories associated with the game
                                var categories = details.Links.Where(x => x.Type == "boardgamecategory").ToList();
                                //Get a list of known categories
                                var gameCategories = _gameService.GetTabletopCategories();
                                foreach (var category in categories)
                                {
                                    //Check to see if the category exists
                                    var categoryExists = gameCategories.Any(x => x.Name == category.value);

                                    int categoryId = -1;

                                    if (!categoryExists)
                                    {
                                        //If it doesn't exist, add it to the main category table
                                        var categoryToAdd = new TabletopCategory()
                                        {
                                            Id = 0,
                                            Name = category.value
                                        };
                                        var categoryAdded = _gameService.AddTabletopCategory(categoryToAdd);

                                        categoryId = categoryAdded.Id;
                                    }

                                    if (categoryId == -1)
                                        categoryId = _gameService.GetTabletopCategoryByName(category.value);

                                    if (categoryId != -1)
                                    {
                                        //Add the category to the game
                                        var gameCategoryToAdd = new TabletopGameCategory()
                                        {
                                            Id = 0,
                                            GameId = game.ObjectId,
                                            TabletopCategoryId = categoryId,
                                            TabletopCategoryName = category.value
                                        };
                                        _gameService.AddTabletopGameCategory(gameCategoryToAdd);
                                    }
                                    else
                                    {

                                    }
                                }

                                //Get a list of categories associated with the game
                                var mechanics = details.Links.Where(x => x.Type == "boardgamemechanic").ToList();
                                //Get a list of known mechanics
                                var gameMechanics = _gameService.GetTabletopMechanics();
                                foreach (var mechanic in mechanics)
                                {
                                    //Check to see if the mechanic exists
                                    var mechanicExists = gameMechanics.Any(x => x.Name == mechanic.value);

                                    int mechanicId = -1;

                                    if (!mechanicExists)
                                    {
                                        //If it doesn't exist, add it to the main mechanic table
                                        var mechanicToAdd = new TabletopMechanic()
                                        {
                                            Id = 0,
                                            Name = mechanic.value
                                        };
                                        var mechanicAdded = _gameService.AddTabletopMechanic(mechanicToAdd);

                                        mechanicId = mechanicAdded.Id;
                                    }

                                    if (mechanicId == -1)
                                        mechanicId = _gameService.GetTabletopMechanicByName(mechanic.value);

                                    if (mechanicId != -1)
                                    {
                                        //Add the mechanic to the game
                                        var gameMechanicToAdd = new TabletopGameMechanic()
                                        {
                                            GameId = game.ObjectId,
                                            MechanicId = mechanicId,
                                            MechanicName = mechanic.value
                                        };
                                        _gameService.AddTabletopGameMechanic(gameMechanicToAdd);
                                    }
                                    else
                                    {

                                    }
                                }
                            }

                            //Add the game to the user
                            var userGame = new UserGame()
                            {
                                Id = 0,
                                UserId = userId,
                                GameId = game.ObjectId,
                                AddedOn = DateTime.Now
                            };
                            _userService.AddGameToUser(userGame);
                        }
                    }
                    else
                    {

                    }
                };
            }
            catch
            {

            }
        }

        public JsonResult GetGameCollection(int userId)
        {
            var model = new List<TabletopGameViewModel>();

            var collection = _userService.GetGamesById(userId);

            foreach (var item in collection)
            {
                var gameData = _gameService.GetTabletopGameById(item.GameId);
                var minPlayers = gameData.MinPlayers.FirstOrDefault();
                var maxPlayers = gameData.MaxPlayers.FirstOrDefault();
                var yearPublished = gameData.YearPublished.FirstOrDefault();
                if (gameData != null)
                {
                    var game = new TabletopGameViewModel()
                    {
                        Id = gameData.TabletopSourceGameId,
                        GameType = gameData.GameType,
                        Name = gameData.Name,
                        Description = gameData.Description,
                        MinPlayers = minPlayers,
                        MaxPlayers = maxPlayers,
                        YearPublished = yearPublished,
                        ImageUrl = gameData.Image,
                        ThumbnailUrl = gameData.ThumbnailImage
                    };

                    var gameCategories = _gameService.GetTabletopCategoriesByGameId(item.GameId);
                    if (gameCategories != null)
                    {
                        foreach (var gameCategory in gameCategories)
                        {
                            var category = new TabletopGameViewModel.Category()
                            {
                                Id = gameCategory.TabletopCategoryId,
                                Name = gameCategory.TabletopCategoryName
                            };
                            game.Categories.Add(category);
                        }
                    }

                    var gameExpansions = _gameService.GetTabletopExpansionsByGameId(item.GameId);
                    if (gameExpansions != null)
                    {
                        foreach (var gameExpansion in gameExpansions)
                        {
                            var expansionDetails = _gameService.GetTabletopGameById(gameExpansion.ExpansionGameId);
                            if (expansionDetails != null)
                            {
                                var expansion = new TabletopGameViewModel.Expansion()
                                {
                                    Id = expansionDetails.Id,
                                    Name = expansionDetails.Name
                                };
                                game.Expansions.Add(expansion);
                            }
                            else
                            {
                                return Json(JsonResponseFactory.ErrorResponse(""), JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    var gameMechanics = _gameService.GetTabletopMechanicsByGameId(item.GameId);
                    if (gameMechanics != null)
                    {
                        foreach (var gameMechanic in gameMechanics)
                        {
                            var mechanic = new TabletopGameViewModel.Mechanic()
                            {
                                Id = gameMechanic.MechanicId,
                                Name = gameMechanic.MechanicName
                            };
                            game.Mechanics.Add(mechanic);
                        }
                    }

                    model.Add(game);
                }
                else
                {
                    return Json(JsonResponseFactory.ErrorResponse(""), JsonRequestBehavior.AllowGet);
                }
            }

            return Json(JsonResponseFactory.SuccessResponse(model), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchBggGames(int userId, string query)
        {
            var model = new List<TabletopGameViewModel>();

            var searchData = _sourceService.SearchBggGames(query);
            var gameCollection = _userService.GetGamesById(userId);

            //Create an empty string array
            IList<string> strings = new List<string>();
            //Add each game's id to the list
            foreach (var game in searchData.Items)
            {
                //Check to see if the game exists in the user's collection
                var gameExistsInCollection = gameCollection.Any(x => x.GameId == game.Id);

                if (!gameExistsInCollection)
                {
                    //Add the game id to the string list
                    string item = game.Id.ToString();
                    strings.Add(item);
                }
            };
            //Convert the game id list to a comma delimited string
            string commaDelimitedString = string.Join(",", strings);
            //Get game details for all games in collection
            var gameDetails = _sourceService.GetGameDetailFromBggByGameId(commaDelimitedString);


            foreach (var item in gameDetails.Items)
            {
                var gameType = item.Names.Select(x => x.Type).FirstOrDefault();
                var name = item.Names.Select(x => x.value).FirstOrDefault();
                var minPlayers = item.MinGamePlayers.Select(x => x.value).FirstOrDefault();
                var maxPlayers = item.MaxGamePlayers.Select(x => x.value).FirstOrDefault();
                var yearPublished = item.YearGamePublished.Select(x => x.value).FirstOrDefault();
                var game = new TabletopGameViewModel()
                {
                    Id = Int32.Parse(item.Id),
                    GameType = gameType,
                    Name = name,
                    Description = item.Description,
                    MinPlayers = int.Parse(minPlayers),
                    MaxPlayers = int.Parse(maxPlayers),
                    ImageUrl = item.Image,
                    ThumbnailUrl = item.Thumbnail,
                    YearPublished = int.Parse(yearPublished)
                };

                var categories = item.Links.Where(x => x.Type == "boardgamecategory");

                if (categories != null)
                {
                    foreach (var category in categories)
                    {
                        var gameCategory = new TabletopGameViewModel.Category()
                        {
                            Id = int.Parse(category.Id),
                            Name = category.value
                        };
                        game.Categories.Add(gameCategory);
                    }
                }

                var expansions = item.Links.Where(x => x.Type == "boardgameexpansion");

                if (expansions != null)
                {
                    foreach (var expansion in expansions)
                    {
                        var gameExpansion = new TabletopGameViewModel.Expansion()
                        {
                            Id = int.Parse(expansion.Id),
                            Name = expansion.value
                        };
                        game.Expansions.Add(gameExpansion);
                    }
                }

                var mechanics = item.Links.Where(x => x.Type == "boardgamemechanic");

                if (mechanics != null)
                {
                    foreach (var mechanic in mechanics)
                    {
                        var gameMechanic = new TabletopGameViewModel.Mechanic()
                        {
                            Id = int.Parse(mechanic.Id),
                            Name = mechanic.value
                        };
                        game.Mechanics.Add(gameMechanic);
                    }
                }

                model.Add(game);
            }

            return Json(JsonResponseFactory.SuccessResponse(model), JsonRequestBehavior.AllowGet);
        }

        public void AddGameToLibrary(int userId, TabletopGameViewModel model)
        {
            var gameExistsInDatabase = _gameService.GetTabletopCategoriesByGameId(model.Id).Any(x => x.GameId == model.Id);
            var gameExistsInLibrary = _userService.GetGamesById(userId).Any(x => x.GameId == model.Id);

            if (!gameExistsInDatabase)
                AddGameToDatabase(model);

            if (!gameExistsInLibrary)
            {
                var userGame = new UserGame()
                {
                    Id = 0,
                    UserId = userId,
                    GameId = model.Id,
                    AddedOn = DateTime.Now
                };
                _userService.AddGameToUser(userGame);
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

        public void AddGameToDatabase(TabletopGameViewModel model)
        {
            var game = new TabletopGame()
            {
                Id = 0,
                TabletopSourceType = (int)TabletopSourceType.BoardGameGeek,
                TabletopSourceGameId = model.Id,
                GameType = model.GameType,
                Name = model.GameType,
                Description = model.Description,
                YearPublished = model.YearPublished.ToString(),
                MinPlayers = model.MinPlayers.ToString(),
                MaxPlayers = model.MaxPlayers.ToString(),
                Image = model.ImageUrl,
                ThumbnailImage = model.ThumbnailUrl,
                AddedOn = DateTime.Now
            };
            _gameService.AddTabletopGame(game);

            if (model.Categories != null)
            {
                var gameCategories = _gameService.GetTabletopCategories();

                foreach (var category in model.Categories)
                {
                    //Determine if the category already exists in the database
                    var categoryExists = gameCategories.Any(x => x.Name == category.Name);
                    var categoryId = -1;
                    if (categoryExists)
                    {
                        //Add the category to the database
                        var categoryToAdd = new TabletopCategory()
                        {
                            Id = 0,
                            Name = category.Name
                        };
                        var categoryAdded = _gameService.AddTabletopCategory(categoryToAdd);
                        categoryId = categoryAdded.Id;
                    }

                    if (categoryId == -1)
                        categoryId = _gameService.GetTabletopCategoryByName(category.Name);

                    if (categoryId != -1)
                    {
                        //Add the category to the game
                        var gameCategory = new TabletopGameCategory()
                        {
                            Id = 0,
                            GameId = game.Id,
                            TabletopCategoryId = categoryId,
                            TabletopCategoryName = category.Name
                        };
                        _gameService.AddTabletopGameCategory(gameCategory);
                    }
                }
            }
            else
            {

            }

            if (model.Expansions != null)
            {
                foreach (var expansion in model.Expansions)
                {
                    //Add each expansion mapping to the expansion table
                    var expansionToAdd = new TabletopGameExpansion()
                    {
                        Id = 0,
                        GameId = game.Id,
                        ExpansionGameId = expansion.Id
                    };
                    _gameService.AddTabletopGameExpansion(expansionToAdd);
                }
            }

            if (model.Mechanics != null)
            {
                var gameMechanics = _gameService.GetTabletopMechanics();

                foreach (var mechanic in model.Mechanics)
                {
                    //Determine if the mechanic already exists in the database
                    var mechanicExists = gameMechanics.Any(x => x.Name == mechanic.Name);
                    var mechanicId = -1;
                    if (mechanicExists)
                    {
                        //Add the mechanic to the database
                        var mechanicToAdd = new TabletopMechanic()
                        {
                            Id = 0,
                            Name = mechanic.Name
                        };
                        var mechanicAdded = _gameService.AddTabletopMechanic(mechanicToAdd);
                        mechanicId = mechanicAdded.Id;
                    }

                    if (mechanicId == -1)
                        mechanicId = _gameService.GetTabletopMechanicByName(mechanic.Name);

                    if (mechanicId != -1)
                    {
                        //Add the mechanic to the game
                        var gameMechanic = new TabletopGameMechanic()
                        {
                            Id = 0,
                            GameId = game.Id,
                            MechanicId = mechanicId,
                            MechanicName = mechanic.Name
                        };
                        _gameService.AddTabletopGameMechanic(gameMechanic);
                    }
                }
            }
            else
            {

            }
        }

        public void RemoveGameFromDatabase(int gameId)
        {
            var game = _gameService.GetTabletopGameById(gameId);

            if (game != null)
            {
                var gameCategories = _gameService.GetTabletopCategoriesByGameId(gameId);
                if (gameCategories != null)
                {
                    foreach (var gameCategory in gameCategories)
                    {
                        _gameService.DeleteTabletopGameCategoryById(gameCategory.Id);
                    }
                }

                var gameExpansions = _gameService.GetTabletopExpansionsByGameId(gameId);
                if (gameExpansions != null)
                {
                    foreach (var gameExpansion in gameExpansions)
                    {
                        _gameService.DeleteTabletopGameExpansionById(gameExpansion.Id);
                    }
                }

                var gameMechanics = _gameService.GetTabletopMechanicsByGameId(gameId);
                if (gameMechanics != null)
                {
                    foreach (var gameMechanic in gameMechanics)
                    {
                        _gameService.DeleteTabletopGameMechanicById(gameMechanic.Id);
                    }
                }

                _gameService.DeleteTabletopGameById(gameId);

            }
            _gameService.DeleteTabletopGameById(gameId);
        }
    }
}