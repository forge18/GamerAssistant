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
        public string ItemType { get; set; }

        public int ItemId { get; set; }

        public string NameType { get; set; }

        public string NameValue { get; set; }

        public int YearPublishedValue { get; set; }
    }
}
