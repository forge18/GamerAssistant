using Abp.Domain.Entities;
using System;

namespace GamerAssistant.Users
{
    public class UserFriend : Entity
    {
        public int UserId { get; set; }

        public int FriendUserId { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
