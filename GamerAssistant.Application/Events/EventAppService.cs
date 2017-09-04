using Abp.Domain.Repositories;

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
    }
}
