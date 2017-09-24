using System;
using System.Collections.Generic;

namespace GamerAssistant.Web.Models.Games
{
    public class TabletopGameViewModel
    {
        public TabletopGameViewModel()
        {
            Categories = new List<Category>();
            Expansions = new List<Expansion>();
            Mechanics = new List<Mechanic>();
            PlayDates = new List<PlayDate>();
        }

        public int Id { get; set; }

        public string GameType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public int YearPublished { get; set; }

        public int PlayTime { get; set; }

        public string OwnerName { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbnailUrl { get; set; }

        public IList<Category> Categories { get; set; }

        public IList<Expansion> Expansions { get; set; }

        public IList<Mechanic> Mechanics { get; set; }

        public IList<PlayDate> PlayDates { get; set; }


        #region Nested Classes
        public class Category
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class Expansion
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class Mechanic
        {
            public int Id { get; set; }

            public string Name { get; set; }
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