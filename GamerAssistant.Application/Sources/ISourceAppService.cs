using Abp.Application.Services;
using System.Collections.Generic;

namespace GamerAssistant.Sources
{
    public interface ISourceAppService : IApplicationService
    {
        BggGameCollection GetBggCollectionByUserId(int userId);

        BggGameDetail GetGameDetailFromBggByGameId(string gameIds);

        BggGameSearch SearchBggGames(string gameName);

        //Get source list by user id
        IList<UserSource> GetSourcesById(int userId);

        //Add source
        void AddSourceToUser(UserSource userSource);

        //Add source
        void UpdateSource(UserSource userSource);

        //Delete Source
        void DeleteSourceById(int userSourceId);
    }
}
