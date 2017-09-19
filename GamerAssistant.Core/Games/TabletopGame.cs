using Abp.Domain.Entities;
using System;

namespace GamerAssistant.Games
{
    public class TabletopGame : Entity
    {
        public int TabletopSourceType { get; set; }

        public int TabletopSourceGameId { get; set; }

        public string GameType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int YearPublished { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public int PlayTime { get; set; }

        public int Image { get; set; }

        public int ThumbnailImage { get; set; }

        public DateTime AddedOn { get; set; }
    }

    public enum TabletopSourceType : int
    {
        ManualEntry = 0,
        BoardGameGeek = 1,
        RpgGeek = 2
    }
}
