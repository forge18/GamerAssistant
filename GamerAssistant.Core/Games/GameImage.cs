using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Games
{
    [Table("game_images")]
    public class GameImage : Entity
    {
        [Column("image_url")]
        public string ImageUrl { get; set; }
    }
}
