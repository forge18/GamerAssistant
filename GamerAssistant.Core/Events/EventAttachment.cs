using Abp.Domain.Entities;
using System;

namespace GamerAssistant.Events
{
    public class EventAttachment : Entity
    {
        public string Description { get; set; }

        public string AttachmentUrl { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int OwnerId { get; set; }
    }
}
