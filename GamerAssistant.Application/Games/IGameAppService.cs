using Abp.Application.Services;
using System.Collections.Generic;

namespace GamerAssistant.Games
{
    public interface IGameAppService : IApplicationService
    {
        //Tabletop Interfaces
        IList<Game> GetGamesList();

        IList<GameCategory> GetCategoriesByGameId(int gameId);

        IList<GameGenre> GetGenresByGameId(int gameId);

        IList<GameMechanic> GetMechanicsByGameId(int gameId);

        IList<GamePlatform> GetPlatformsByGameId(int gameId);

        IList<Category> GetCategories();

        IList<Genre> GetGenres();

        IList<Mechanic> GetMechanics();

        IList<Platform> GetPlatforms();

        Game GetGameById(int gameId);

        int GetCategoryByName(string name);

        int GetMechanicByName(string name);

        Game AddGame(Game game);

        void UpdateGame(Game game);

        void DeleteGameById(int gameId);

        Category AddCategory(Category category);

        void DeleteCategory(int categoryId);

        void AddGameCategory(GameCategory gameCategory);

        void DeleteGameCategoryById(int gameCategoryId);

        Mechanic AddMechanic(Mechanic mechanic);

        void DeleteMechanic(int mechanicId);

        void AddGameMechanic(GameMechanic gameMechanic);

        void DeleteGameMechanicById(int gameMechanicId);

        Genre AddGenre(Genre genre);

        void AddGameGenre(GameGenre gameGenre);

        void DeleteGameGenreById(int gameGenreId);

        Platform AddPlatform(Platform platform);

        void AddGamePlatform(GamePlatform gamePlatform);

        void DeleteGamePlatformById(int gamePlatformId);
    }
}
