using System.Collections.Generic;

namespace GamerAssistant.Web.Models.Admin
{
    public class AdminViewModel
    {
        public AdminViewModel()
        {
            Categories = new List<Category>();
            Mechanics = new List<Mechanic>();
            Themes = new List<Theme>();
        }

        public IList<Category> Categories { get; set; }

        public IList<Mechanic> Mechanics { get; set; }

        public IList<Theme> Themes { get; set; }


        #region Nested Classes

        public class Category
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class Mechanic
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class Theme
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        #endregion
    }
}