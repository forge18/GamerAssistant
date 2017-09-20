using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GamerAssistant.Games
{
    public class GameAppService : IGameAppService
    {
        private readonly IRepository<TabletopCategory> _tabletopCategoryRepository;
        private readonly IRepository<TabletopMechanic> _tabletopMechanicRepository;
        private readonly IRepository<TabletopGame> _tabletopGameRepository;
        private readonly IRepository<TabletopGameExpansion> _tabletopGameExpansionRepository;
        private readonly IRepository<TabletopGameCategory> _tabletopGameCategoryRepository;
        private readonly IRepository<TabletopGameMechanic> _tabletopGameMechanicRepository;
        private readonly IRepository<VideoCategory> _videoCategoryRepository;
        private readonly IRepository<VideoGenre> _videoGenreRepository;
        private readonly IRepository<VideoPlatform> _videoPlatformRepository;
        private readonly IRepository<VideoGame> _videoGameRepository;
        private readonly IRepository<VideoGameCategory> _videoGameCategoryRepository;
        private readonly IRepository<VideoGameGenre> _videoGameGenreRepository;
        private readonly IRepository<VideoGamePlatform> _videoGamePlatformRepository;


        public GameAppService(
            IRepository<TabletopCategory> tabletopCategoryRepository,
            IRepository<TabletopMechanic> tabletopMechanicRepository,
            IRepository<TabletopGame> tabletopGameRepository,
            IRepository<TabletopGameExpansion> tabletopGameExpansionRepository,
            IRepository<TabletopGameCategory> tabletopGameCategoryRepository,
            IRepository<TabletopGameMechanic> tabletopGameMechanicRepository,
            IRepository<VideoCategory> videoCategoryRepository,
            IRepository<VideoGenre> videoGenreRepository,
            IRepository<VideoPlatform> videoPlatformRepository,
            IRepository<VideoGame> videoGameRepository,
            IRepository<VideoGameCategory> videoGameCategoryRepository,
            IRepository<VideoGameGenre> videoGameGenreRepository,
            IRepository<VideoGamePlatform> videoGamePlatformRepository
        )
        {
            _tabletopCategoryRepository = tabletopCategoryRepository;
            _tabletopMechanicRepository = tabletopMechanicRepository;
            _tabletopGameRepository = tabletopGameRepository;
            _tabletopGameExpansionRepository = tabletopGameExpansionRepository;
            _tabletopGameCategoryRepository = tabletopGameCategoryRepository;
            _tabletopGameMechanicRepository = tabletopGameMechanicRepository;
            _videoCategoryRepository = videoCategoryRepository;
            _videoGenreRepository = videoGenreRepository;
            _videoPlatformRepository = videoPlatformRepository;
            _videoGameRepository = videoGameRepository;
            _videoGameCategoryRepository = videoGameCategoryRepository;
            _videoGameGenreRepository = videoGameGenreRepository;
            _videoGamePlatformRepository = videoGamePlatformRepository;
        }

        public IList<TabletopGame> GetTabletopGamesList()
        {
            var games = _tabletopGameRepository.GetAll().ToList();
            if (games == null)
                return null;

            return games;
        }

        public IList<TabletopGameExpansion> GetTabletopExpansionsByGameId(int gameId)
        {
            var gameExpansions = _tabletopGameExpansionRepository.GetAll().Where(x => x.GameId == gameId).ToList();

            if (gameExpansions == null)
                return null;

            return gameExpansions;
        }

        public IList<TabletopGameCategory> GetTabletopCategoriesByGameId(int gameId)
        {
            var gameCategories = _tabletopGameCategoryRepository.GetAll().Where(x => x.Id == gameId).ToList();

            if (gameCategories == null)
                return null;

            return gameCategories;
        }

        public IList<TabletopGameMechanic> GetTabletopMechanicsByGameId(int gameId)
        {
            var gameMechanics = _tabletopGameMechanicRepository.GetAll().Where(x => x.Id == gameId).ToList();

            if (gameMechanics == null)
                return null;

            return gameMechanics;
        }

        public IList<TabletopCategory> GetTabletopCategories()
        {
            var gameCategories = _tabletopCategoryRepository.GetAll().ToList();

            if (gameCategories == null)
                return null;

            return gameCategories;
        }

        public IList<TabletopMechanic> GetTabletopMechanics()
        {
            var gameMechanics = _tabletopMechanicRepository.GetAll().ToList();

            if (gameMechanics == null)
                return null;

            return gameMechanics;
        }

        public void AddTabletopGame(TabletopGame game)
        {
            _tabletopGameRepository.Insert(game);
        }

        public void UpdateTabletopGame(TabletopGame game)
        {
            _tabletopGameRepository.Update(game);
        }

        public void DeleteTabletopGameById(int gameId)
        {
            _tabletopGameRepository.Delete(gameId);
        }

        public void AddTabletopGameCategory(TabletopGameCategory gameCategory)
        {
            _tabletopGameCategoryRepository.Insert(gameCategory);
        }

        public void DeleteTabletopGameCategoryById(int gameCategoryId)
        {
            _tabletopGameCategoryRepository.Delete(gameCategoryId);
        }

        public void AddTabletopGameMechanic(TabletopGameMechanic gameMechanic)
        {
            _tabletopGameMechanicRepository.Insert(gameMechanic);
        }

        public void DeleteTabletopGameMechanicById(int gameMechanicId)
        {
            _tabletopGameMechanicRepository.Delete(gameMechanicId);
        }

        public void AddTabletopGameExpansion(TabletopGameExpansion gameExpansion)
        {
            _tabletopGameExpansionRepository.Insert(gameExpansion);
        }

        public void DeleteTabletopGameExpansionById(int gameExpansionId)
        {
            _tabletopGameExpansionRepository.Delete(gameExpansionId);
        }

        public IList<VideoGame> GetVideoGamesList()
        {
            var games = _videoGameRepository.GetAll().ToList();
            if (games == null)
                return null;

            return games;
        }

        public IList<VideoGamePlatform> GetVideoGamePlatformsByGameId(int gameId)
        {
            var gamePlatforms = _videoGamePlatformRepository.GetAll().Where(x => x.GameId == gameId).ToList();

            if (gamePlatforms == null)
                return null;

            return gamePlatforms;
        }

        public IList<VideoGameCategory> GetVideoGameCategoriesByGameId(int gameId)
        {
            var gameCategories = _videoGameCategoryRepository.GetAll().Where(x => x.GameId == gameId).ToList();

            if (gameCategories == null)
                return null;

            return gameCategories;
        }

        public IList<VideoGameGenre> GetVideoGameGenresByGameId(int gameId)
        {
            var gameGenres = _videoGameGenreRepository.GetAll().Where(x => x.GameId == gameId).ToList();

            if (gameGenres == null)
                return null;

            return gameGenres;
        }

        public IList<VideoPlatform> GetVideoPlatforms()
        {
            var platforms = _videoPlatformRepository.GetAll().ToList();

            if (platforms == null)
                return null;

            return platforms;
        }

        public IList<VideoCategory> GetVideoCategories()
        {
            var categories = _videoCategoryRepository.GetAll().ToList();

            if (categories == null)
                return null;

            return categories;
        }

        public IList<VideoGenre> GetVideoGenres()
        {
            var genres = _videoGenreRepository.GetAll().ToList();

            if (genres == null)
                return null;

            return genres;
        }

        public void AddVideoGame(VideoGame game)
        {
            _videoGameRepository.Insert(game);
        }

        public void UpdateVideoGame(VideoGame game)
        {
            _videoGameRepository.Update(game);
        }

        public void DeleteVideoGameById(int gameId)
        {
            _videoGameRepository.Delete(gameId);
        }

        public void AddVideoGameCategory(VideoGameCategory gameCategory)
        {
            _videoGameCategoryRepository.Insert(gameCategory);
        }

        public void DeleteVideoGameCategoryById(int gameCategoryId)
        {
            _videoGameCategoryRepository.Delete(gameCategoryId);
        }

        public void AddVideoGameGenre(VideoGameGenre gameGenre)
        {
            _videoGameGenreRepository.Insert(gameGenre);
        }

        public void DeleteVideoGameGenreById(int gameGenreId)
        {
            _videoGameGenreRepository.Delete(gameGenreId);
        }

        public void AddVideoGamePlatform(VideoGamePlatform gamePlatform)
        {
            _videoGamePlatformRepository.Insert(gamePlatform);
        }

        public void DeleteVideoGamePlatformById(int gamePlatformId)
        {
            _videoGamePlatformRepository.Delete(gamePlatformId);
        }

    }
}
