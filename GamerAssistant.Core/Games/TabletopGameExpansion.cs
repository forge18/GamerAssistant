using Abp.Domain.Entities;

namespace GamerAssistant.Games
{
    public class TabletopGameExpansion : Entity
    {
        public int GameId { get; set; }

        public int ExpansionGameId { get; set; }
    }
}
