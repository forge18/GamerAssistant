using Abp.Application.Services;
using System.Collections.Generic;

namespace GamerAssistant.Events
{
    public interface IEventAppService : IApplicationService
    {
        IList<Event> GetEventsList();

        IList<EventAttachment> GetAttachmentsByEventId(int eventId);

        IList<EventDateOption> GetDateOptionsByEventId(int eventId);

        IList<EventGame> GetGamesByEventId(int eventId);

        IList<EventTask> GetTasksByEventId(int eventId);

        IList<EventUser> GetUsersByEventId(int eventId);

        void AddEvent(Event eventItem);

        void UpdateEvent(Event eventItem);

        void CancelEventById(int eventId);

        void AddAttachmentToEvent(EventAttachment eventAttachment);

        void DeleteAttachmentFromEventById(int attachmentId);

        void AddDateOptionToEvent(EventDateOption dateOption);

        void DeleteDateOptionFromEventById(int dateOptionEventId);

        void AddGameToEvent(EventGame eventGame);

        void DeleteGameFromEventById(int eventGameId);

        void AddTask(EventTask task);

        void UpdateTask(EventTask task);

        void DeleteTaskById(int taskId);

        void AddUserToEvent(EventUser eventUser);

        void DeleteUserFromEventById(int eventUserId);
    }
}
