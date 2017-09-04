using Abp.Domain.Repositories;

namespace GamerAssistant.Admin
{
    public class AdminAppService : IAdminAppService
    {
        private readonly IRepository<GameCategory> _gameCategoryRepository;
        private readonly IRepository<GameMechanic> _gameMechanicRepository;
        private readonly IRepository<GameTheme> _gameThemeRepository;

        public AdminAppService(
            IRepository<GameCategory> gameCategoryRepository,
            IRepository<GameMechanic> gameMechanicRepository,
            IRepository<GameTheme> gameThemeRepository
        )
        {
            _gameCategoryRepository = gameCategoryRepository;
            _gameMechanicRepository = gameMechanicRepository;
            _gameThemeRepository = gameThemeRepository;
        }
    }
}
