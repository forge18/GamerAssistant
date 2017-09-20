using Abp.Domain.Entities;

namespace GamerAssistant.Games
{
    public class VideoGamePlatform : Entity
    {
        public int GameId { get; set; }

        public int VideoPlatformId { get; set; }

        public string VideoPlatformName { get; set; }
    }
}
