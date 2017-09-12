using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Games
{
    [Table("games")]
    public class Game : Entity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("min_players")]
        public int MinPlayers { get; set; }

        [Column("max_players")]
        public int MaxPlayers { get; set; }

        [Column("game_type")]
        public GameType GameType { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("theme_id")]
        public int ThemeId { get; set; }

        [Column("primary_mechanic_id")]
        public int PrimaryMechanicId { get; set; }

        [Column("secondary_mechanic_id")]
        public int SecondaryMechanicId { get; set; }

        [Column("primary_image_url")]
        public string PrimaryImageUrl { get; set; }

        [Column("publisher_url")]
        public string PublisherUrl { get; set; }

        [Column("added_on")]
        public DateTime AddedOn { get; set; }
    }

    public enum GameType : int
    {
        Computer = 1,
        Table = 2,
        Roleplaying = 3,
        Card = 4
    }
}
