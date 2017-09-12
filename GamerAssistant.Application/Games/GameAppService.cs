using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GamerAssistant.Games
{
    public class GameAppService : IGameAppService
    {
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<GameExpansion> _gameExpansionRepository;
        private readonly IRepository<GameImage> _gameImageRepository;
        private readonly IRepository<GamePlayDate> _gamePlayDateRepository;

        public GameAppService(
            IRepository<Game> gameRepository,
            IRepository<GameExpansion> gameExpansionRepository,
            IRepository<GameImage> gameImageRepository,
            IRepository<GamePlayDate> gamePlayDateRepository
        )
        {
            _gameRepository = gameRepository;
            _gameExpansionRepository = gameExpansionRepository;
            _gameImageRepository = gameImageRepository;
            _gamePlayDateRepository = gamePlayDateRepository;
        }

        public IList<Game> GetGamesMasterList()
        {
            var games = _gameRepository.GetAll().ToList();
            if (games == null)
                return null;

            return games;
        }

        public IList<GameExpansion> GetExpansionsByGameId(int gameId)
        {
            var gameExpansions = _gameExpansionRepository.GetAll().Where(x => x.GameId == gameId).ToList();

            if (gameExpansions == null)
                return null;

            return gameExpansions;
        }

        public IList<GameImage> GetImagesByGameId(int gameId)
        {
            var gameImages = _gameImageRepository.GetAll().Where(x => x.Id == gameId).ToList();

            if (gameImages == null)
                return null;

            return gameImages;
        }

        public IList<GamePlayDate> GetPlayDatesByGameId(int gameId)
        {
            var gamePlayDates = _gamePlayDateRepository.GetAll().Where(x => x.GameId == gameId).ToList();

            if (gamePlayDates == null)
                return null;

            return gamePlayDates;
        }

        public void AddGameMaster(Game game)
        {
            _gameRepository.Insert(game);
        }

        public void UpdateGameMaster(Game game)
        {
            _gameRepository.Update(game);
        }

        public void DeleteGameMasterById(int gameId)
        {
            _gameRepository.Delete(gameId);
        }

        public void AddExpansion(GameExpansion gameExpansion)
        {
            _gameExpansionRepository.Insert(gameExpansion);
        }

        public void UpdateExpansion(GameExpansion gameExpansion)
        {
            _gameExpansionRepository.Update(gameExpansion);
        }

        public void DeleteExpansionById(int expansionId)
        {
            _gameExpansionRepository.Delete(expansionId);
        }

        public void UpdateImageByGameId(int gameId, string imageLink)
        {
            var gameImage = new GameImage()
            {
                Id = gameId,
                ImageUrl = imageLink
            };

            _gameImageRepository.Update(gameImage);
        }

        public void AddPlayDateById(GamePlayDate gamePlayDate)
        {
            _gamePlayDateRepository.Insert(gamePlayDate);
        }

        public void DeletePlayDateById(int playDateId)
        {
            _gamePlayDateRepository.Delete(playDateId);
        }

    }
}
