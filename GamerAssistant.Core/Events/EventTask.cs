using Abp.Domain.Entities;
using System;

namespace GamerAssistant.Events
{
    public class EventTask : Entity
    {
        public int EventId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Instructions { get; set; }

        public string CompletionComments { get; set; }

        public DateTime CompletedOn { get; set; }

    }
}
