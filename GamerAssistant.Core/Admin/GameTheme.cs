using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Admin
{
    [Table("game_themes")]
    public class GameTheme : Entity
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
