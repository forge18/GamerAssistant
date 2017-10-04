using GamerAssistant.Authorization.Users;
using GamerAssistant.Events;
using GamerAssistant.Games;
using GamerAssistant.Users;
using GamerAssistant.Web.Models.Events;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GamerAssistant.Web.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventAppService _eventService;
        private readonly GameAppService _gameService;
        private readonly UserManager _userManager;
        private readonly UserAppService _userService;

        public EventsController(
            EventAppService eventService,
            GameAppService gameService,
            UserManager userManager,
            UserAppService userService
        )
        {
            _eventService = eventService;
            _gameService = gameService;
            _userManager = userManager;
            _userService = userService;
        }

        // GET: Events
        public ActionResult Index()
        {
            return View();
        }

        public List<EventListViewModel> GetEventList()
        {
            var model = new List<EventListViewModel>();

            var events = _eventService.GetEventsList();

            foreach (var item in events)
            {
                var eventItem = new EventListViewModel()
                {
                    EventId = item.Id,
                    Description = item.Description,
                    StartDateTime = item.StartDateTime,
                    EndDateTime = item.EndDateTime,
                    LocationNickname = item.LocationNickname,
                    Address = item.Address,
                    City = item.City,
                    State = item.State,
                    Zip = item.Zip
                };

                eventItem.EventInvitees = GetEventInvitees(item.Id);
                eventItem.Games = GetEventGames(item.Id);
                eventItem.Tasks = GetEventTasks(item.Id);
                eventItem.DateOptions = GetEventProposedDates(item.Id);
                eventItem.Attachments = GetEventAttachments(item.Id);

                model.Add(eventItem);
            }

            return model;
        }

        public List<EventListViewModel.EventInvitee> GetEventInvitees(int eventId)
        {
            var model = new List<EventListViewModel.EventInvitee>();

            var invitees = _eventService.GetUsersByEventId(eventId);

            if (invitees != null && invitees.Count > 0)
            {
                foreach (var invitee in invitees)
                {
                    var user = _userManager.GetUserByIdAsync(invitee.UserId);

                    var eventInvitee = new EventListViewModel.EventInvitee()
                    {
                        Id = invitee.Id,
                        UserId = invitee.UserId,
                        UserName = user.Result.UserName,
                        Accepted = invitee.Accepted,
                        Attending = invitee.Attended,
                        DeclineComment = invitee.DeclineComment
                    };

                    model.Add(eventInvitee);
                }
            }

            return model;
        }

        public List<EventListViewModel.Game> GetEventGames(int eventId)
        {
            var model = new List<EventListViewModel.Game>();

            var games = _eventService.GetGamesByEventId(eventId);

            if (games != null && games.Count > 0)
            {
                foreach (var game in games)
                {
                    var gameName = _gameService.GetGameById(game.GameId);

                    if (gameName != null)
                    {
                        var eventGame = new EventListViewModel.Game()
                        {
                            Id = game.Id,
                            EventId = game.EventId,
                            GameId = game.GameId,
                            GameName = gameName.Name,
                            StatusId = (int)game.Status,
                            StatusName = game.Status.ToString()
                        };

                        var votes = _userService.GetVotesByEventGameId(game.Id);

                        foreach (var vote in votes)
                        {
                            var user = _userManager.GetUserByIdAsync(vote.UserId);

                            var gameVote = new EventListViewModel.Vote()
                            {
                                Id = vote.Id,
                                UserId = vote.UserId,
                                UserName = user.Result.UserName,
                                VoteStatus = (int)vote.Vote,
                                DueDate = vote.DueDate
                            };

                            eventGame.Votes.Add(gameVote);
                        }

                        model.Add(eventGame);
                    }
                }
            }

            return model;
        }

        public List<EventListViewModel.Task> GetEventTasks(int eventId)
        {
            var model = new List<EventListViewModel.Task>();

            var tasks = _eventService.GetTasksByEventId(eventId);

            if (tasks != null && tasks.Count > 0)
            {
                foreach (var task in tasks)
                {
                    var user = _userManager.GetUserByIdAsync(task.UserId);

                    var eventTask = new EventListViewModel.Task()
                    {
                        Id = task.Id,
                        Name = task.Name,
                        Description = task.Instructions,
                        UserId = task.UserId,
                        UserName = user.Result.UserName,
                        CompletionComments = task.CompletionComments,
                        CompletedOn = task.CompletedOn
                    };

                    model.Add(eventTask);
                }
            }

            return model;
        }

        public List<EventListViewModel.DateOption> GetEventProposedDates(int eventId)
        {
            var model = new List<EventListViewModel.DateOption>();

            var dates = _eventService.GetDateOptionsByEventId(eventId);

            if (dates != null && dates.Count > 0)
            {
                foreach (var item in dates)
                {
                    var user = _userManager.GetUserByIdAsync(item.OwnerId);

                    var eventDate = new EventListViewModel.DateOption()
                    {
                        Id = item.Id,
                        StartDate = item.StartDateTime,
                        EndDate = item.EndDateTime,
                        OwnerId = item.OwnerId,
                        OwnerName = user.Result.UserName
                    };

                    model.Add(eventDate);
                }
            }

            return model;
        }

        public List<EventListViewModel.Attachment> GetEventAttachments(int eventId)
        {
            var model = new List<EventListViewModel.Attachment>();

            var attachments = _eventService.GetAttachmentsByEventId(eventId);

            if (attachments != null && attachments.Count > 0)
            {
                foreach (var attachment in attachments)
                {
                    var user = _userManager.GetUserByIdAsync(attachment.OwnerId);

                    var eventAttachment = new EventListViewModel.Attachment()
                    {
                        Id = attachment.Id,
                        AttachmentUrl = attachment.AttachmentUrl,
                        Description = attachment.Description,
                        AddedOn = attachment.UpdatedOn,
                        OwnerId = attachment.OwnerId,
                        OwnerName = user.Result.UserName
                    };

                    model.Add(eventAttachment);
                }
            }

            return model;
        }

        public void AddUpdateEvent(EventListViewModel model)
        {

        }

        public void DeleteEvent(int eventId)
        {

        }

        public void AddUpdateEventInvitee(EventListViewModel.EventInvitee invitee)
        {
            var item = new EventUser()
            {
                EventId = invitee.EventId,
                UserId = invitee.UserId,
                UpdatedOn = DateTime.Now,
                Responded = false,
                Accepted = false,
                Attended = false,
                DeclineComment = null
            };

            _eventService.AddUserToEvent(item);
        }

        public void DeleteEventInvitee(int eventInviteeId)
        {
            _eventService.DeleteUserFromEventById(eventInviteeId);
        }

        public void AddEventGame(EventListViewModel.Game game)
        {
            var item = new EventGame()
            {
                EventId = game.EventId,
                GameId = game.GameId,
                Status = GameStatus.Suggested
            };

            _eventService.AddGameToEvent(item);
        }

        public void DeleteEventGame(int eventGameId)
        {
            _eventService.DeleteGameFromEventById(eventGameId);
        }

        public void AddUpdateEventTask(EventListViewModel.Task task)
        {
            var item = new EventTask()
            {
                EventId = task.Id,
                UserId = task.UserId,
                Name = task.Name,
                Instructions = task.Description,
                CompletionComments = task.CompletionComments,
                CompletedOn = task.CompletedOn
            };

            if (task.Id == 0)
                _eventService.AddTask(item);
            else
                _eventService.UpdateTask(item);
        }

        public void DeleteEventTask(int eventTaskId)
        {
            _eventService.DeleteTaskById(eventTaskId);
        }

        public void AddEventProposedDates(EventListViewModel.DateOption proposedDate)
        {
            var item = new EventProposal()
            {
                StartDateTime = proposedDate.StartDate,
                EndDateTime = proposedDate.EndDate,
                OwnerId = proposedDate.OwnerId
            };

            _eventService.AddDateOptionToEvent(item);
        }

        public void DeleteEventProposedDate(int eventDateId)
        {
            _eventService.DeleteDateOptionFromEventById(eventDateId);
        }

        public void AddUpdateEventAttachments(EventListViewModel.Attachment attachment)
        {
            var item = new EventAttachment()
            {
                Description = attachment.Description,
                AttachmentUrl = attachment.AttachmentUrl,
                UpdatedOn = attachment.AddedOn,
                OwnerId = attachment.OwnerId
            };

            _eventService.AddAttachmentToEvent(item);
        }

        public void DeleteEventAttachment(int eventAttachmentId)
        {
            _eventService.DeleteAttachmentFromEventById(eventAttachmentId);
        }

        public void AcceptEventInvite(int eventInviteId)
        {
            var eventInvite = _eventService.GetEventInviteById(eventInviteId);

            eventInvite.Responded = true;
            eventInvite.Accepted = true;

            _eventService.UpdateEventInvite(eventInvite);
        }

        public void DeclineEventInvite(int eventInviteId, string declineComment)
        {
            var eventInvite = _eventService.GetEventInviteById(eventInviteId);

            eventInvite.Responded = true;
            eventInvite.Accepted = false;
            eventInvite.DeclineComment = declineComment;

            _eventService.UpdateEventInvite(eventInvite);
        }

        public void UpdateEventStatus(int eventId, int statusId)
        {
            var eventItem = _eventService.GetEventById(eventId);

            eventItem.Status = (EventStatus)statusId;

            _eventService.UpdateEvent(eventItem);
        }

        public void AddVote(UserVote userVote)
        {
            _userService.AddVoteToUser(userVote);
        }

        public void UpdateVote(int userVoteId, int vote)
        {

            var userVote = _userService.GetVoteById(userVoteId);

            userVote.Vote = (VoteStatus)vote;
            userVote.UpdatedOn = DateTime.Now;

            _userService.UpdateVote(userVote);
        }

        public void DeleteVote(int userVoteId)
        {
            _userService.DeleteVoteById(userVoteId);
        }
    }
}