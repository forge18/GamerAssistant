using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Admin
{
    [Table("game_categories")]
    public class GameCategory : Entity
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
