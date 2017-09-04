using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Users
{
    [Table("user_favorites")]
    public class UserFavorite : Entity
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("game_id")]
        public int GameId { get; set; }

        [Column("comment")]
        public string Comment { get; set; }
    }
}
