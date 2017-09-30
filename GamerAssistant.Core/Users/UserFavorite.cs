using Abp.Domain.Entities;

namespace GamerAssistant.Users
{
    public class UserFavorite : Entity
    {
        public int UserId { get; set; }

        public int GameId { get; set; }
    }
}
