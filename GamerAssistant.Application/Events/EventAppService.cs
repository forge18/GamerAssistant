using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GamerAssistant.Events
{
    public class EventAppService : IEventAppService
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly IRepository<EventAttachment> _eventAttachmentRepository;
        private readonly IRepository<EventDateOption> _eventDateOptionRepository;
        private readonly IRepository<EventGame> _eventGameRepository;
        private readonly IRepository<EventTask> _eventTaskRepository;
        private readonly IRepository<EventUser> _eventUserRepository;

        public EventAppService(
            IRepository<Event> eventRepository,
            IRepository<EventAttachment> eventAttachmentRepository,
            IRepository<EventDateOption> eventDateOptionRepository,
            IRepository<EventGame> eventGameRepository,
            IRepository<EventTask> eventTaskRepository,
            IRepository<EventUser> eventUserRepository
        )
        {
            _eventRepository = eventRepository;
            _eventAttachmentRepository = eventAttachmentRepository;
            _eventDateOptionRepository = eventDateOptionRepository;
            _eventGameRepository = eventGameRepository;
            _eventTaskRepository = eventTaskRepository;
            _eventUserRepository = eventUserRepository;
        }

        public IList<Event> GetEventsList()
        {
            var events = _eventRepository.GetAll().ToList();
            if (events == null)
                return null;

            return events;
        }

        public IList<EventAttachment> GetAttachmentsByEventId(int eventId)
        {
            var attachments = _eventAttachmentRepository.GetAll().ToList();
            if (attachments == null)
                return null;

            return attachments;
        }

        public IList<EventDateOption> GetDateOptionsByEventId(int eventId)
        {
            var dateOptions = _eventDateOptionRepository.GetAll().ToList();
            if (dateOptions == null)
                return null;

            return dateOptions;
        }

        public IList<EventGame> GetGamesByEventId(int eventId)
        {
            var games = _eventGameRepository.GetAll().ToList();
            if (games == null)
                return null;

            return games;
        }

        public IList<EventTask> GetTasksByEventId(int eventId)
        {
            var tasks = _eventTaskRepository.GetAll().ToList();
            if (tasks == null)
                return null;

            return tasks;
        }

        public IList<EventUser> GetUsersByEventId(int eventId)
        {
            var users = _eventUserRepository.GetAll().ToList();
            if (users == null)
                return null;

            return users;
        }

        public void AddEvent(Event eventItem)
        {
            _eventRepository.Insert(eventItem);
        }

        public void UpdateEvent(Event eventItem)
        {
            _eventRepository.Update(eventItem);
        }

        public void CancelEventById(int eventId)
        {
            _eventRepository.Delete(eventId);
        }

        public void AddAttachmentToEvent(EventAttachment eventAttachment)
        {
            _eventAttachmentRepository.Insert(eventAttachment);
        }

        public void DeleteAttachmentFromEventById(int attachmentId)
        {
            _eventAttachmentRepository.Delete(attachmentId);
        }

        public void AddDateOptionToEvent(EventDateOption dateOption)
        {
            _eventDateOptionRepository.Insert(dateOption);
        }

        public void DeleteDateOptionFromEventById(int dateOptionEventId)
        {
            _eventDateOptionRepository.Delete(dateOptionEventId);
        }

        public void AddGameToEvent(EventGame eventGame)
        {
            _eventGameRepository.Insert(eventGame);
        }

        public void DeleteGameFromEventById(int eventGameId)
        {
            _eventGameRepository.Delete(eventGameId);
        }

        public void AddTask(EventTask task)
        {
            _eventTaskRepository.Insert(task);
        }

        public void UpdateTask(EventTask task)
        {
            _eventTaskRepository.Update(task);
        }

        public void DeleteTaskById(int taskId)
        {
            _eventTaskRepository.Delete(taskId);
        }

        public void AddUserToEvent(EventUser eventUser)
        {
            _eventUserRepository.Insert(eventUser);
        }

        public void DeleteUserFromEventById(int eventUserId)
        {
            _eventUserRepository.Delete(eventUserId);
        }

    }
}
