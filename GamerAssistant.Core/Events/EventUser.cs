using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Events
{
    [Table("event_users")]
    public class EventUser : Entity
    {
        [Column("event_id")]
        public int EventId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("attending")]
        public bool Attending { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("updated_on")]
        public DateTime UpdatedOn { get; set; }
    }
}
