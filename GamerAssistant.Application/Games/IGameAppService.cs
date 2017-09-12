using Abp.Application.Services;
using System.Collections.Generic;

namespace GamerAssistant.Games
{
    public interface IGameAppService : IApplicationService
    {
        IList<Game> GetGamesMasterList();

        IList<GameExpansion> GetExpansionsByGameId(int gameId);

        IList<GameImage> GetImagesByGameId(int gameId);

        IList<GamePlayDate> GetPlayDatesByGameId(int gameId);

        void AddGameMaster(Game game);

        void UpdateGameMaster(Game game);

        void DeleteGameMasterById(int gameId);

        void AddExpansion(GameExpansion expansion);

        void UpdateExpansion(GameExpansion expansion);

        void DeleteExpansionById(int expansionId);

        void UpdateImageByGameId(int gameId, string imageLink);

        void AddPlayDateById(GamePlayDate gamePlayDate);

        void DeletePlayDateById(int playDateId);
    }
}
