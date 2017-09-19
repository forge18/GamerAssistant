using Abp.Domain.Entities;

namespace GamerAssistant.Events
{
    public class EventGame : Entity
    {
        public int EventId { get; set; }

        public int GameId { get; set; }

        public GameStatus Status { get; set; }
    }

    public enum GameStatus : int
    {
        Suggested = 1,
        Approved = 2
    }
}
