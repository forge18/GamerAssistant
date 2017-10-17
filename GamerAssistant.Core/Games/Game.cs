using Abp.Domain.Entities;
using System;

namespace GamerAssistant.Games
{
    public class Game : Entity
    {
        public int TabletopSourceType { get; set; }

        public int TabletopSourceGameId { get; set; }

        public string GameType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string YearPublished { get; set; }

        public string MinPlayers { get; set; }

        public string MaxPlayers { get; set; }

        public string PlayTime { get; set; }

        public string Image { get; set; }

        public string ThumbnailImage { get; set; }

        public bool IsExpansion { get; set; }

        public int SourceParentGameId { get; set; }

        public DateTime AddedOn { get; set; }
    }

    public enum TabletopSourceType : int
    {
        ManualEntry = 0,
        BoardGameGeek = 1,
        Steam = 2
    }
}
