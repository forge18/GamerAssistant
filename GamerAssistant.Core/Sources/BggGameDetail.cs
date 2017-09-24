using System.Collections.Generic;

namespace GamerAssistant.Sources
{
    public class BggGameDetail
    {
        public List<Item> Items { get; set; }

        public class Item
        {
            public string Type { get; set; }

            public string Id { get; set; }

            public string Thumbnail { get; set; }

            public string Image { get; set; }

            public List<Name> Names { get; set; }

            public string Description { get; set; }

            public List<YearPublished> YearGamePublished { get; set; }

            public List<MinPlayers> MinGamePlayers { get; set; }

            public List<MaxPlayers> MaxGamePlayers { get; set; }

            public List<Link> Links { get; set; }
        }

        public class MinPlayers
        {
            public string value { get; set; }
        }

        public class MaxPlayers
        {
            public string value { get; set; }
        }

        public class YearPublished
        {
            public string value { get; set; }
        }

        public class Name
        {
            public string Type { get; set; }

            public string SortIndex { get; set; }

            public string value { get; set; }
        }

        public class Link
        {
            public string Type { get; set; }

            public string Id { get; set; }

            public string value { get; set; }
        }
    }
}

