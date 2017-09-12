using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Users
{
    [Table("user_games")]
    public class UserGame : Entity
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("game_id")]
        public int GameId { get; set; }

        [Column("expansion_id")]
        public int ExpansionId { get; set; }

        [Column("added_on")]
        public DateTime AddedOn { get; set; }
    }
}
