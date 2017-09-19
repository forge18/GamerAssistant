using Abp.Domain.Entities;

namespace GamerAssistant.Users
{
    public class UserFavorite : Entity
    {
        public int UserId { get; set; }

        public int TabletopGameId { get; set; }

        public int VideoGameId { get; set; }

        public string Comment { get; set; }
    }
}
