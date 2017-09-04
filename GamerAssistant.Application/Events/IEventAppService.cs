using Abp.Application.Services;
using System;
using System.Collections.Generic;

namespace GamerAssistant.Events
{
    public interface IEventAppService : IApplicationService
    {
        IList<Event> GetEventsList();

        IList<EventAttachment> GetAttachmentsByEventId();

        IList<EventDateOption> GetDateOptiosnByEventId();

        IList<EventGame> GetGamesByEventId();

        IList<EventTask> GetTasksByEventId();

        IList<EventUser> GetUsersByEventId();

        void AddEvent(Event eventItem);

        void CancelEvent(int eventId);

        void AddAttachmentByEventId(int eventId, string link);

        void DeleteAttachmentById(int attachmentId);

        void AddDateOptionByEventId(int eventId, DateTime dateOption);

        void DeleteDateOptionById(int dateOptionEventId);

        void AddGameByEventId(int eventId, int gameId);

        void DeleteGameById(int gameEventId);

        void AddTaskByEventId(int eventId, EventTask task);

        void UpdateTaskByEventId(int eventId, EventTask task);

        void DeleteTaskById(int gameEventId);

        void AddUserByEventId(int eventId, int userId);

        void DeleteUserById(int userEventId);
    }
}
