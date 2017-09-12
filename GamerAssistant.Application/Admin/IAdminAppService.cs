using Abp.Application.Services;
using System.Collections.Generic;

namespace GamerAssistant.Admin
{
    public interface IAdminAppService : IApplicationService
    {
        IList<GameCategory> GetCategoryList();

        IList<GameMechanic> GetMechanicList();

        IList<GameTheme> GetThemeList();

        void AddCategory(GameCategory category);

        void UpdateCategory(GameCategory category);

        void DeleteCategoryById(int categoryId);

        void AddMechanic(GameMechanic mechanic);

        void UpdateMechanic(GameMechanic mechanic);

        void DeleteMechanicById(int mechanicId);

        void AddTheme(GameTheme theme);

        void UpdateTheme(GameTheme theme);

        void DeleteThemeById(int themeId);
    }
}
