using Abp.Domain.Entities;

namespace GamerAssistant.Games
{
    public class VideoGameGenre : Entity
    {
        public int GameId { get; set; }

        public int VideoGenreId { get; set; }

        public string VideoGenreName { get; set; }
    }
}
