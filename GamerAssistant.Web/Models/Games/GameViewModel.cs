using System;
using System.Collections.Generic;

namespace GamerAssistant.Web.Models.Games
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            Categories = new List<Category>();
            Expansions = new List<Expansion>();
            Genres = new List<Genre>();
            Mechanics = new List<Mechanic>();
            Owners = new List<Owner>();
            Platforms = new List<Platform>();
            PlayDates = new List<PlayDate>();
        }

        public int Id { get; set; }

        public string GameType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public string PlayerRange { get; set; }

        public int YearPublished { get; set; }

        public string PlayTime { get; set; }

        public string OwnerName { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbnailUrl { get; set; }

        public bool IsExpansion { get; set; }

        public int ParentGameId { get; set; }

        public IList<Category> Categories { get; set; }

        public IList<Expansion> Expansions { get; set; }

        public IList<Genre> Genres { get; set; }

        public IList<Mechanic> Mechanics { get; set; }

        public IList<Owner> Owners { get; set; }

        public IList<Platform> Platforms { get; set; }

        public IList<PlayDate> PlayDates { get; set; }

        public bool ShowDetails { get; set; }


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

        public class Genre
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class Mechanic
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class Owner
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class Platform
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