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

        public IList<Game> GetGameList()
        {
            var games = _gameRepository.GetAll().ToList();
            if (games == null)
                return null;

            return games;
        }
    }
}
