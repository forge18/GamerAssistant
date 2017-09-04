using System;
using System.Collections.Generic;

namespace GamerAssistant.Web.Models.Games
{
    public class GameListViewModel
    {
        public GameListViewModel()
        {
            Expansions = new List<Expansion>();
            Images = new List<Image>();
            PlayDates = new List<PlayDate>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public int ThemeId { get; set; }

        public string ThemeName { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int PrimaryMechanicId { get; set; }

        public string PrimaryMechanicName { get; set; }

        public int SecondaryMechanicId { get; set; }

        public string SecondaryMechanicName { get; set; }

        public string PrimaryImageUrl { get; set; }

        public int OwnerId { get; set; }

        public string OwnerName { get; set; }



        public IList<Expansion> Expansions { get; set; }

        public IList<Image> Images { get; set; }

        public IList<PlayDate> PlayDates { get; set; }


        #region Nested Classes

        public class Expansion
        {
            public string Name { get; set; }
        }

        public class Image
        {
            public string ImageUrl { get; set; }
        }

        public class PlayDate
        {
            public DateTime Date { get; set; }

            public int NumberOfPlayers { get; set; }

            public int Rating { get; set; }
        }

        #endregion 
    }
}