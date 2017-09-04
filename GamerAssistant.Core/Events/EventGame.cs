using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Events
{
    [Table("events_games")]
    public class EventGame : Entity
    {
        [Column("event_id")]
        public int EventId { get; set; }

        [Column("game_id")]
        public int GameId { get; set; }

        [Column("status")]
        public GameStatus Status { get; set; }
    }

    public enum GameStatus : int
    {
        Suggested = 1,
        Approved = 2
    }
}
