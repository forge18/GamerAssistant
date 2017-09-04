using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Games
{
    [Table("game_play_dates")]
    public class GamePlayDate : Entity
    {
        [Column("game_id")]
        public int GameId { get; set; }

        [Column("play_date")]
        public DateTime PlayDate { get; set; }

        [Column("number_of_players")]
        public int NumberOfPlayers { get; set; }

        [Column("rating")]
        public int Rating { get; set; }
    }
}
