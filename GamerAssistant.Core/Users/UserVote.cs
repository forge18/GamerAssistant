using Abp.Domain.Entities;
using System;

namespace GamerAssistant.Users
{
    public class UserVote : Entity
    {
        public int UserId { get; set; }

        public int EventDateOptionId { get; set; }

        public int EventGameId { get; set; }

        public DateTime DueDate { get; set; }

        public VoteStatus Vote { get; set; }

        public DateTime UpdatedOn { get; set; }
    }

    public enum VoteStatus : byte
    {

        Nay = 0,
        Yay = 1
    }
}
