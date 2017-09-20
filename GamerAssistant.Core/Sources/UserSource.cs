using Abp.Domain.Entities;

namespace GamerAssistant.Sources
{
    public class UserSource : Entity
    {
        public int UserId { get; set; }

        public int SourceType { get; set; }

        public string SourceUserName { get; set; }

        public string SourceAuth { get; set; }
    }
}
