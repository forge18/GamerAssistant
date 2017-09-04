using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Events
{
    [Table("events")]
    public class Event : Entity
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("location")]
        public string Location { get; set; }

        [Column("start_date")]
        public DateTime StartDateTime { get; set; }

        [Column("end_date")]
        public DateTime EndDateTime { get; set; }

        [Column("status")]
        public EventStatus Status { get; set; }
    }

    public enum EventStatus : int
    {
        Scheduled = 1,
        Cancelled = 2
    }
}
