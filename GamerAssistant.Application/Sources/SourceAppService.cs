using Abp.Domain.Repositories;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GamerAssistant.Sources
{
    public class SourceAppService : ISourceAppService
    {
        private readonly IRepository<UserSource> _userSourceRepository;

        public SourceAppService(
            IRepository<UserSource> userSourceRepository
        )
        {
            _userSourceRepository = userSourceRepository;
        }


        public BggGameCollection GetBggCollectionByUserId(int userId)
        {
            try
            {
                var collection = new BggGameCollection();
                //Find the source user name in the usersource table
                var userName = _userSourceRepository.FirstOrDefault(x => x.UserId == userId).SourceUserName;

                //Determine if the username exists
                if (userName != null)
                {
                    //Create the api url
                    var resource = "xmlapi2/collection?username=" + userName;

                    //Create a new client
                    var client = new RestClient();
                    //Give the client a base url
                    client.BaseUrl = new Uri("http://www.boardgamegeek.com");
                    //Create a new request
                    var request = new RestRequest();
                    //Stuff the api url into the request
                    request.Resource = resource;

                    var successfulCall = false;
                    //This will ensure that the api times out after ten seconds
                    TimeSpan timeSpan = DateTime.Now.AddSeconds(11) - DateTime.Now;
                    int elapsed = 0;
                    while (!successfulCall && elapsed < timeSpan.TotalMilliseconds)
                    {
                        //Api only gets called every five seconds for max of 3 times.
                        if (elapsed == 0 || elapsed == 5000 || elapsed == 1000)
                        {
                            //Send response data back to controller
                            var response = client.Execute<BggGameCollection>(request);

                            if (response.ResponseStatus.ToString() == "200")
                            {
                                collection = response.Data;
                                successfulCall = true;
                            }
                            else if (response.ResponseStatus.ToString() == "204")
                                successfulCall = true;
                        }

                        Thread.Sleep(1000);
                        elapsed += 1000;
                    }

                    return collection;
                }
                else
                    return collection;
            }
            catch
            {
                return null;
            }
        }

        public BggGameDetail GetGameDetailFromBgg(int gameId)
        {
            var game = new BggGameDetail();


            return game;
        }

        public BggGameSearch SearchBggGames(string gameName)
        {
            var results = new BggGameSearch();

            return results;
        }

        public IList<UserSource> GetSourcesById(int userId)
        {
            var userSources = _userSourceRepository.GetAll().ToList();
            if (userSources == null)
                return null;

            return userSources;
        }

        public void AddSourceToUser(UserSource userSource)
        {
            _userSourceRepository.Insert(userSource);
        }

        public void UpdateSource(UserSource userSource)
        {
            _userSourceRepository.Update(userSource);
        }

        public void DeleteSourceById(int userSourceId)
        {
            _userSourceRepository.Delete(userSourceId);
        }
    }
}
