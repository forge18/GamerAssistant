using Abp.Domain.Entities;
using System;

namespace GamerAssistant.Events
{
    public class Event : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string LocationNickname { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public EventStatus Status { get; set; }
    }

    public enum EventStatus : int
    {
        Proposed = 1,
        Scheduled = 2,
        Cancelled = 3

    }
}
