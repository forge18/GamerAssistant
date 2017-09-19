using Abp.Domain.Entities;

namespace GamerAssistant.Games
{
    public class TabletopGameExpansion : Entity
    {
        public int GameId { get; set; }

        public int ExpansionGameId { get; set; }

        public string Name { get; set; }

        public int YearPublished { get; set; }

        public int MinPlayer { get; set; }

        public int MaxPlayer { get; set; }

        public string ThumbnailImage { get; set; }
    }
}
