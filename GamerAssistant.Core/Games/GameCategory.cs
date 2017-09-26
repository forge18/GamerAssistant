using Abp.Domain.Entities;

namespace GamerAssistant.Games
{
    public class GameCategory : Entity
    {
        public int GameId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
