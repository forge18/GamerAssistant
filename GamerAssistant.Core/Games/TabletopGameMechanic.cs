using Abp.Domain.Entities;

namespace GamerAssistant.Games
{
    public class TabletopGameMechanic : Entity
    {
        public int GameId { get; set; }

        public int MechanicId { get; set; }

        public string MechanicName { get; set; }
    }
}
