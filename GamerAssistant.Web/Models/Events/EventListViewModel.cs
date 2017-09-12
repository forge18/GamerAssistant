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
            EventUsers = new List<EventUser>();
        }

        public int EventId { get; set; }

        public string Description { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Location { get; set; }



        public IList<Attachment> Attachments { get; set; }

        public IList<DateOption> DateOptions { get; set; }

        public IList<Game> Games { get; set; }

        public IList<Task> Tasks { get; set; }

        public IList<EventUser> EventUsers { get; set; }



        #region Nested Classes

        public class Attachment
        {
            public string AttachmentUrl { get; set; }

            public DateTime UpdatedOn { get; set; }

            public int OwnerId { get; set; }

            public string OwnerName { get; set; }
        }

        public class DateOption
        {
            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }

            public string Location { get; set; }

            public int OwnerId { get; set; }

            public string OwnerName { get; set; }
        }

        public class Game
        {
            public int GameId { get; set; }

            public string GameName { get; set; }

            public int StatusId { get; set; }

            public string StatusName { get; set; }

            public IList<Vote> Votes { get; set; }
        }

        public class Task
        {
            public int TaskId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public int UserId { get; set; }

            public string UserName { get; set; }
        }

        public class EventUser
        {
            public int UserId { get; set; }

            public string UserName { get; set; }

            public bool Attending { get; set; }
        }

        public class Vote
        {
            public int UserId { get; set; }

            public string UserName { get; set; }

            public int VoteStatus { get; set; }
        }

        #endregion
    }
}