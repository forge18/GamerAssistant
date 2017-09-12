using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

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

        public IList<GameCategory> GetCategoryList()
        {
            var categories = _gameCategoryRepository.GetAll().ToList();
            if (categories == null)
                return null;

            return categories;
        }

        public IList<GameMechanic> GetMechanicList()
        {
            var mechanics = _gameMechanicRepository.GetAll().ToList();
            if (mechanics == null)
                return null;

            return mechanics;
        }

        public IList<GameTheme> GetThemeList()
        {
            var themes = _gameThemeRepository.GetAll().ToList();
            if (themes == null)
                return null;

            return themes;
        }

        public void AddCategory(GameCategory category)
        {
            _gameCategoryRepository.Insert(category);
        }

        public void UpdateCategory(GameCategory category)
        {
            _gameCategoryRepository.Update(category);
        }

        public void DeleteCategoryById(int categoryId)
        {
            _gameCategoryRepository.Delete(categoryId);
        }

        public void AddMechanic(GameMechanic mechanic)
        {
            _gameMechanicRepository.Insert(mechanic);
        }

        public void UpdateMechanic(GameMechanic mechanic)
        {
            _gameMechanicRepository.Update(mechanic);
        }

        public void DeleteMechanicById(int mechanicId)
        {
            _gameMechanicRepository.Delete(mechanicId);
        }

        public void AddTheme(GameTheme theme)
        {
            _gameThemeRepository.Insert(theme);
        }

        public void UpdateTheme(GameTheme theme)
        {
            _gameThemeRepository.Update(theme);
        }

        public void DeleteThemeById(int themeId)
        {
            _gameThemeRepository.Delete(themeId);
        }

    }
}
