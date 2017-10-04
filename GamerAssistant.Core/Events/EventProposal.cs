using Abp.Domain.Entities;
using System;

namespace GamerAssistant.Events
{
    public class EventProposal : Entity
    {
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int OwnerId { get; set; }
    }
}
