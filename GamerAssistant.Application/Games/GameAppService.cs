using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GamerAssistant.Games
{
    public class GameAppService : IGameAppService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Mechanic> _mechanicRepository;
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<GameCategory> _gameCategoryRepository;
        private readonly IRepository<GameMechanic> _gameMechanicRepository;
        private readonly IRepository<Genre> _genreRepository;
        private readonly IRepository<Platform> _platformRepository;
        private readonly IRepository<GameGenre> _gameGenreRepository;
        private readonly IRepository<GamePlatform> _gamePlatformRepository;


        public GameAppService(
            IRepository<Category> categoryRepository,
            IRepository<Mechanic> mechanicRepository,
            IRepository<Game> gameRepository,
            IRepository<GameCategory> gameCategoryRepository,
            IRepository<GameMechanic> gameMechanicRepository,
            IRepository<Genre> genreRepository,
            IRepository<Platform> platformRepository,
            IRepository<GameGenre> gameGenreRepository,
            IRepository<GamePlatform> gamePlatformRepository
        )
        {
            _categoryRepository = categoryRepository;
            _mechanicRepository = mechanicRepository;
            _gameRepository = gameRepository;
            _gameCategoryRepository = gameCategoryRepository;
            _gameMechanicRepository = gameMechanicRepository;
            _genreRepository = genreRepository;
            _platformRepository = platformRepository;
            _gameGenreRepository = gameGenreRepository;
            _gamePlatformRepository = gamePlatformRepository;
        }

        public IList<Game> GetGamesList()
        {
            var games = _gameRepository.GetAll().ToList();
            if (games == null)
                return null;

            return games;
        }

        public IList<GameCategory> GetCategoriesByGameId(int gameId)
        {
            var gameCategories = _gameCategoryRepository.GetAll().Where(x => x.GameId == gameId).ToList();

            if (gameCategories == null)
                return null;

            return gameCategories;
        }

        public IList<GameMechanic> GetMechanicsByGameId(int gameId)
        {
            var gameMechanics = _gameMechanicRepository.GetAll().Where(x => x.GameId == gameId).ToList();

            if (gameMechanics == null)
                return null;

            return gameMechanics;
        }

        public IList<Category> GetCategories()
        {
            var gameCategories = _categoryRepository.GetAll().ToList();

            if (gameCategories == null)
                return null;

            return gameCategories;
        }

        public IList<Mechanic> GetMechanics()
        {
            var gameMechanics = _mechanicRepository.GetAll().ToList();

            if (gameMechanics == null)
                return null;

            return gameMechanics;
        }

        public Game GetGameById(int gameId)
        {
            var game = _gameRepository.GetAll().FirstOrDefault(x => x.Id == gameId);

            if (game == null)
                return null;

            return game;
        }

        public int GetCategoryByName(string name)
        {
            var category = _categoryRepository.GetAll().FirstOrDefault(x => x.Name == name).Id;

            if (category == 0)
                return -1;

            return category;
        }

        public int GetGenreByName(string name)
        {
            var genre = _genreRepository.GetAll().FirstOrDefault(x => x.Name == name).Id;

            if (genre == 0)
                return -1;

            return genre;
        }

        public int GetMechanicByName(string name)
        {
            var mechanic = _mechanicRepository.GetAll().FirstOrDefault(x => x.Name == name).Id;

            if (mechanic == 0)
                return -1;

            return mechanic;
        }

        public int GetPlatformByName(string name)
        {
            var platform = _platformRepository.GetAll().FirstOrDefault(x => x.Name == name).Id;

            if (platform == 0)
                return -1;

            return platform;
        }

        public Game AddGame(Game game)
        {
            _gameRepository.InsertAndGetId(game);

            return game;
        }

        public void UpdateGame(Game game)
        {
            _gameRepository.Update(game);
        }

        public void DeleteGameById(int gameId)
        {
            _gameRepository.Delete(gameId);
        }

        public Category AddCategory(Category category)
        {
            _categoryRepository.InsertAndGetId(category);

            return category;
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryRepository.Delete(categoryId);
        }

        public void AddGameCategory(GameCategory gameCategory)
        {
            _gameCategoryRepository.InsertAndGetId(gameCategory);
        }

        public void DeleteGameCategoryById(int gameCategoryId)
        {
            _gameCategoryRepository.Delete(gameCategoryId);
        }

        public Mechanic AddMechanic(Mechanic mechanic)
        {
            _mechanicRepository.InsertAndGetId(mechanic);

            return mechanic;
        }

        public void DeleteMechanic(int mechanicId)
        {
            _mechanicRepository.Delete(mechanicId);
        }

        public void AddGameMechanic(GameMechanic gameMechanic)
        {
            _gameMechanicRepository.InsertAndGetId(gameMechanic);
        }

        public void DeleteGameMechanicById(int gameMechanicId)
        {
            _gameMechanicRepository.Delete(gameMechanicId);
        }

        public IList<GamePlatform> GetPlatformsByGameId(int gameId)
        {
            var gamePlatforms = _gamePlatformRepository.GetAll().Where(x => x.GameId == gameId).ToList();

            if (gamePlatforms == null)
                return null;

            return gamePlatforms;
        }

        public IList<GameGenre> GetGenresByGameId(int gameId)
        {
            var gameGenres = _gameGenreRepository.GetAll().Where(x => x.GameId == gameId).ToList();

            if (gameGenres == null)
                return null;

            return gameGenres;
        }

        public IList<Platform> GetPlatforms()
        {
            var platforms = _platformRepository.GetAll().ToList();

            if (platforms == null)
                return null;

            return platforms;
        }

        public IList<Genre> GetGenres()
        {
            var genres = _genreRepository.GetAll().ToList();

            if (genres == null)
                return null;

            return genres;
        }

        public Genre AddGenre(Genre genre)
        {
            _genreRepository.InsertAndGetId(genre);

            return genre;
        }

        public void AddGameGenre(GameGenre gameGenre)
        {
            _gameGenreRepository.Insert(gameGenre);
        }

        public void DeleteGameGenreById(int gameGenreId)
        {
            _gameGenreRepository.Delete(gameGenreId);
        }

        public Platform AddPlatform(Platform platform)
        {
            _platformRepository.InsertAndGetId(platform);

            return platform;
        }

        public void AddGamePlatform(GamePlatform gamePlatform)
        {
            _gamePlatformRepository.Insert(gamePlatform);
        }

        public void DeleteGamePlatformById(int gamePlatformId)
        {
            _gamePlatformRepository.Delete(gamePlatformId);
        }

    }
}
