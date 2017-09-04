using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Admin
{
    [Table("game_mechanics")]
    public class GameMechanic : Entity
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
