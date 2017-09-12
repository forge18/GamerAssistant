using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Users
{
    [Table("user_friends")]
    public class UserFriend : Entity
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("friend_user_id")]
        public int FriendUserId { get; set; }

        [Column("added_on")]
        public DateTime AddedOn { get; set; }
    }
}
