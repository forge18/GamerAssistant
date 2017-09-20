using Abp.Domain.Entities;

namespace GamerAssistant.Games
{
    public class TabletopGameCategory : Entity
    {
        public int GameId { get; set; }

        public int TabletopCategoryId { get; set; }

        public string TabletopCategoryName { get; set; }
    }
}
