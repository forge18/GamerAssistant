﻿using System.Collections.Generic;

namespace GamerAssistant.Sources
{
    public class BggGameCollection
    {
        public List<Item> Items { get; set; }

        public int TotalItems { get; set; }

        public string PubDate { get; set; }

        public class Item
        {
            public string ObjectType { get; set; }

            public int ObjectId { get; set; }

            public string SubType { get; set; }

            public string Name { get; set; }

            public string YearPublished { get; set; }

            public string Image { get; set; }

            public string Thumbnail { get; set; }

            public int NumPlays { get; set; }
        }
    }
}
