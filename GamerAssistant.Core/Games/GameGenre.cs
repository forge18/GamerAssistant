using Abp.Domain.Entities;

namespace GamerAssistant.Games
{
    public class GameGenre : Entity
    {
        public int GameId { get; set; }

        public int GenreId { get; set; }

        public string GenreName { get; set; }
    }
}
