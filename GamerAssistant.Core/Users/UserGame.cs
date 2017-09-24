using Abp.Domain.Entities;
using System;

namespace GamerAssistant.Users
{
    public class UserGame : Entity
    {
        public int UserId { get; set; }

        public int GameId { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
