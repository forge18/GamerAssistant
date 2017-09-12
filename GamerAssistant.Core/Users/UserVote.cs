using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Users
{
    [Table("user_votes")]
    public class UserVote : Entity
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("event_date_option_id")]
        public int EventDateOptionId { get; set; }

        [Column("event_game_id")]
        public int EventGameId { get; set; }

        [Column("due_date")]
        public DateTime DueDate { get; set; }

        [Column("vote")]
        public VoteStatus Vote { get; set; }

        [Column("updated_on")]
        public DateTime UpdatedOn { get; set; }
    }

    public enum VoteStatus : byte
    {

        Nay = 0,
        Yay = 1
    }
}
