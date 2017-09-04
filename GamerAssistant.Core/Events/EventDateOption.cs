using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Events
{
    [Table("event_date_options")]
    public class EventDateOption : Entity
    {
        [Column("start_date")]
        public DateTime StartDateTime { get; set; }

        [Column("end_date")]
        public DateTime EndDateTime { get; set; }

        [Column("location")]
        public string Location { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("owner_id")]
        public int OwnerId { get; set; }
    }
}
