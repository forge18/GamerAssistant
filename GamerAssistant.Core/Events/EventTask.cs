using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Events
{
    [Table("event_tasks")]
    public class EventTask : Entity
    {
        [Column("event_id")]
        public int EventId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("completed_on")]
        public DateTime CompletedOn { get; set; }

    }
}
