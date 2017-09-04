using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Games
{
    [Table("game_expansions")]
    public class GameExpansion : Entity
    {
        [Column("game_id")]
        public int GameId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("added_on")]
        public DateTime AddedOn { get; set; }

        [Column("owner_id")]
        public int OwnerId { get; set; }
    }
}
