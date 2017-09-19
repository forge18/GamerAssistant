using Abp.Domain.Entities;
using System;

namespace GamerAssistant.Events
{
    public class EventUser : Entity
    {
        public int EventId { get; set; }

        public int UserId { get; set; }

        public bool Accepted { get; set; }

        public bool Attended { get; set; }

        public string DeclineComment { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
