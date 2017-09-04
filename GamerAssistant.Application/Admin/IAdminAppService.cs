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

        void DeleteCategory(int categoryId);

        void AddMechanic(GameMechanic mechanic);

        void DeleteMechanic(int mechanicId);

        void AddTheme(GameTheme theme);

        void DeleteTheme(int themeId);
    }
}
