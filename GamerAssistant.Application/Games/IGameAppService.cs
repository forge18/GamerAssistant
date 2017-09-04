using Abp.Application.Services;
using System;
using System.Collections.Generic;

namespace GamerAssistant.Games
{
    public interface IGameAppService : IApplicationService
    {
        IList<Game> GetGamesList();

        IList<GameExpansion> GetExpansionByGameId();

        IList<GameImage> GetImageByGameId();

        IList<GamePlayDate> GetPlayDatesByGameId();

        void AddGame(Game game);

        void DeleteGame(int gameId);

        void AddExpansionByGameId(GameExpansion expansion);

        void DeleteExpansionById(int expansionId);

        void UpdateImageByGameId(int gameId, string imageLink);

        void AddPlayDate(int gameId, DateTime playDate);

        void DeletePlayDateById(int playDateId);
    }
}
