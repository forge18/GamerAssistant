using Abp.Domain.Entities;

namespace GamerAssistant.Games
{
    public class VideoGameCategory : Entity
    {
        public int GameId { get; set; }

        public int VideoCategoryId { get; set; }

        public string VideoCategoryName { get; set; }
    }
}
