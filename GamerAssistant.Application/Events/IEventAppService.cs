using Abp.Application.Services;
using System.Collections.Generic;

namespace GamerAssistant.Events
{
    public interface IEventAppService : IApplicationService
    {
        IList<Event> GetEventsList();

        Event GetEventById(int eventId);

        IList<EventGame> GetEventsByGameId(int gameId);

        IList<EventAttachment> GetAttachmentsByEventId(int eventId);

        IList<EventProposal> GetDateProposalByEventId(int eventId);

        IList<EventGame> GetGamesByEventId(int eventId);

        IList<EventTask> GetTasksByEventId(int eventId);

        IList<EventInvite> GetInvitesByEventId(int eventId);

        EventInvite GetEventInviteById(int eventUserId);

        void AddEvent(Event eventItem);

        void UpdateEvent(Event eventItem);

        void DeleteEventById(int eventId);

        void AddAttachmentToEvent(EventAttachment eventAttachment);

        void DeleteAttachmentFromEventById(int attachmentId);

        void AddDateProposalToEvent(EventProposal dateOption);

        void DeleteDateProposalFromEventById(int dateOptionEventId);

        void AddGameToEvent(EventGame eventGame);

        void DeleteGameFromEventById(int eventGameId);

        void AddTask(EventTask task);

        void UpdateTask(EventTask task);

        void DeleteTaskById(int taskId);

        void AddInviteToEvent(EventInvite eventUser);

        void DeleteInviteFromEventById(int eventUserId);
    }
}
