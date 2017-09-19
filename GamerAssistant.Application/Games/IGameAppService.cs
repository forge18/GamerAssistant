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

        void AddTabletopGame(TabletopGame game);

        void UpdateTabletopGame(TabletopGame game);

        void DeleteTabletopGameById(int gameId);

        void AddTabletopGameCategory(TabletopGameCategory gameCategory);

        void DeleteToptopGameCategoryById(int id);

        void AddTabletopGameMechanic(TabletopGameMechanic gameMechanic);

        void DeleteTabletopGameMechanicById(int id);

        void AddTabletopGameExpansion(TabletopGameExpansion game);

        void DeleteToptopGameExpansionById(int id);


        //Video Game Interfaces
        IList<VideoGame> GetVideoGamesList();

        IList<VideoGamePlatform> GetVideoPlatformsByGameId(int gameId);

        IList<VideoGameCategory> GetVideoCategoriesByGameId(int gameId);

        IList<VideoGameGenre> GetVideoGenressByGameId(int gameId);

        IList<VideoPlatform> GetVideoPlatforms();

        IList<VideoCategory> GetVideoCategories();

        IList<VideoGenre> GetVideoGenres();

        void AddVideoGame(VideoGame game);

        void UpdateVideoGame(VideoGame game);

        void DeleteVideoGameById(int gameId);

        void AddVideoGameCategory(VideoGameCategory gameCategory);

        void DeleteVideoGameCategoryById(int id);

        void AddVideoGameGenre(VideoGameGenre gameGenre);

        void DeleteVideoGameGenreById(int id);

        void AddVideoGamePlatform(VideoGamePlatform gamePlatform);

        void DeleteVideoGamePlatformById(int id);
    }
}
