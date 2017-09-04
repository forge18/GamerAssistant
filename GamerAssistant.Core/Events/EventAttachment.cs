using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerAssistant.Events
{
    [Table("event_attachments")]
    public class EventAttachment : Entity
    {
        [Column("description")]
        public string Description { get; set; }

        [Column("attachment_url")]
        public string AttachmentUrl { get; set; }

        [Column("updated_on")]
        public DateTime UpdatedOn { get; set; }

        [Column("owner_id")]
        public int OwnerId { get; set; }
    }
}
