using System.Collections.Generic;

namespace GamerAssistant.Sources
{
    public class BggGameSearch
    {
        public List<Item> Items { get; set; }

        public int Total { get; set; }

        public string TermsOfUse { get; set; }
    }

    public class Item
    {
        public string Type { get; set; }

        public int Id { get; set; }

        public List<Name> Names { get; set; }

        public List<YearPublished> YearGamePublished { get; set; }
    }

    public class Name
    {
        public string Type { get; set; }

        public string value { get; set; }
    }

    public class YearPublished
    {
        public string value { get; set; }
    }
}

