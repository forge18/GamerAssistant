using Abp.Domain.Entities;
using System;

namespace GamerAssistant.Users
{
    public class UserGame : Entity
    {
        public int UserId { get; set; }

        public int TabletopGameId { get; set; }

        public int VideoGameId { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
