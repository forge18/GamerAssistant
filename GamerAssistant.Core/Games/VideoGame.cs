using Abp.Domain.Entities;

namespace GamerAssistant.Games
{
    public class VideoGame : Entity
    {
        public int VideoSourceType { get; set; }

        public int VideoSourceGameId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ReleaseDate { get; set; }

        public string Image { get; set; }
    }

    public enum VideoSourceType : int
    {
        Other = 0,
        Steam = 1,
        Blizzard = 2,
        Epic = 3,
        Origin = 4
    }
}
