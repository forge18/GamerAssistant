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

        [Column("tabletop_game_id")]
        public int TabletopGameId { get; set; }

        [Column("video_game_id")]
        public int VideoGameId { get; set; }

        [Column("added_on")]
        public DateTime AddedOn { get; set; }
    }
}
