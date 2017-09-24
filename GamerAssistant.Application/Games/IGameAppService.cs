using Abp.Application.Services;
using System.Collections.Generic;

namespace GamerAssistant.Games
{
    public interface IGameAppService : IApplicationService
    {
        //Tabletop Interfaces
        IList<TabletopGame> GetTabletopGamesList();

        IList<TabletopGameExpansion> GetTabletopExpansionsByGameId(int gameId);

        IList<TabletopGameCategory> GetTabletopCategoriesByGameId(int gameId);

        IList<TabletopGameMechanic> GetTabletopMechanicsByGameId(int gameId);

        IList<TabletopCategory> GetTabletopCategories();

        IList<TabletopMechanic> GetTabletopMechanics();

        TabletopGame GetTabletopGameById(int gameId);

        int GetTabletopCategoryByName(string name);

        int GetTabletopMechanicByName(string name);

        void AddTabletopGame(TabletopGame game);

        void UpdateTabletopGame(TabletopGame game);

        void DeleteTabletopGameById(int gameId);

        TabletopCategory AddTabletopCategory(TabletopCategory category);

        void DeleteTabletopCategory(int categoryId);

        void AddTabletopGameCategory(TabletopGameCategory gameCategory);

        void DeleteTabletopGameCategoryById(int gameCategoryId);

        TabletopMechanic AddTabletopMechanic(TabletopMechanic mechanic);

        void DeleteTabletopMechanic(int mechanicId);

        void AddTabletopGameMechanic(TabletopGameMechanic gameMechanic);

        void DeleteTabletopGameMechanicById(int gameMechanicId);

        void AddTabletopGameExpansion(TabletopGameExpansion game);

        void DeleteTabletopGameExpansionById(int gameExpansionId);


        //Video Game Interfaces
        IList<VideoGame> GetVideoGamesList();

        IList<VideoGamePlatform> GetVideoGamePlatformsByGameId(int gameId);

        IList<VideoGameCategory> GetVideoGameCategoriesByGameId(int gameId);

        IList<VideoGameGenre> GetVideoGameGenresByGameId(int gameId);

        IList<VideoPlatform> GetVideoPlatforms();

        IList<VideoCategory> GetVideoCategories();

        IList<VideoGenre> GetVideoGenres();

        void AddVideoGame(VideoGame game);

        void UpdateVideoGame(VideoGame game);

        void DeleteVideoGameById(int gameId);

        void AddVideoGameCategory(VideoGameCategory gameCategory);

        void DeleteVideoGameCategoryById(int gameCategoryId);

        void AddVideoGameGenre(VideoGameGenre gameGenre);

        void DeleteVideoGameGenreById(int gameGenreId);

        void AddVideoGamePlatform(VideoGamePlatform gamePlatform);

        void DeleteVideoGamePlatformById(int gamePlatformId);
    }
}
