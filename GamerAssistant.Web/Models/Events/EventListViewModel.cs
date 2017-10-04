using System;
using System.Collections.Generic;

namespace GamerAssistant.Web.Models.Events
{
    public class EventListViewModel
    {
        public EventListViewModel()
        {
            Attachments = new List<Attachment>();
            DateOptions = new List<DateOption>();
            Games = new List<Game>();
            Tasks = new List<Task>();
            EventInvitees = new List<EventInvitee>();
        }

        public int EventId { get; set; }

        public string Description { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public string LocationNickname { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }



        public IList<Attachment> Attachments { get; set; }

        public IList<DateOption> DateOptions { get; set; }

        public IList<Game> Games { get; set; }

        public IList<Task> Tasks { get; set; }

        public IList<EventInvitee> EventInvitees { get; set; }



        #region Nested Classes

        public class Attachment
        {
            public int Id { get; set; }

            public string Description { get; set; }

            public string AttachmentUrl { get; set; }

            public DateTime AddedOn { get; set; }

            public int OwnerId { get; set; }

            public string OwnerName { get; set; }
        }

        public class DateOption
        {
            public int Id { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }

            public int OwnerId { get; set; }

            public string OwnerName { get; set; }
        }

        public class Game
        {
            public int Id { get; set; }

            public int EventId { get; set; }

            public int GameId { get; set; }

            public string GameName { get; set; }

            public int StatusId { get; set; }

            public string StatusName { get; set; }

            public IList<Vote> Votes { get; set; }
        }

        public class Task
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public int UserId { get; set; }

            public string UserName { get; set; }

            public string CompletionComments { get; set; }

            public DateTime CompletedOn { get; set; }
        }

        public class EventInvitee
        {
            public int Id { get; set; }

            public int EventId { get; set; }

            public int UserId { get; set; }

            public string UserName { get; set; }

            public bool Responded { get; set; }

            public bool Accepted { get; set; }

            public bool Attending { get; set; }

            public string DeclineComment { get; set; }
        }

        public class Vote
        {
            public int Id { get; set; }

            public int UserId { get; set; }

            public string UserName { get; set; }

            public int VoteStatus { get; set; }

            public DateTime DueDate { get; set; }
        }

        #endregion
    }
}