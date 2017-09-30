using System.Collections.Generic;

namespace GamerAssistant.Web.Models.Games
{
    public class GameListsViewModel
    {
        public GameListsViewModel()
        {
            BggGames = new List<GameViewModel>();
            SteamGames = new List<GameViewModel>();
            FriendsGames = new List<GameViewModel>();
            FavoriteGames = new List<GameViewModel>();
        }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<GameViewModel> BggGames { get; set; }

        public IList<GameViewModel> SteamGames { get; set; }

        public IList<GameViewModel> FriendsGames { get; set; }

        public IList<GameViewModel> FavoriteGames { get; set; }
    }
}