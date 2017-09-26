using Abp.Domain.Entities;

namespace GamerAssistant.Games
{
    public class GamePlatform : Entity
    {
        public int GameId { get; set; }

        public int PlatformId { get; set; }

        public string PlatformName { get; set; }
    }
}
