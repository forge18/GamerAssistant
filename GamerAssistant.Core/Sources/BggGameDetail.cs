using System.Collections.Generic;

namespace GamerAssistant.Sources
{
    public class BggGameDetail
    {
        public List<Item> Items { get; set; }

        public class Item
        {
            public string Type { get; set; }

            public int Id { get; set; }

            public string Thumbnail { get; set; }

            public string Image { get; set; }

            public string NameType { get; set; }

            public string NameValue { get; set; }

            public string Description { get; set; }

            public string YearPublished { get; set; }

            public int MinPlayers { get; set; }

            public int MaxPlayers { get; set; }

            public List<Link> Links { get; set; }
        }

        public class Link
        {
            public string Type { get; set; }

            public int Id { get; set; }

            public string Value { get; set; }
        }
    }
}
