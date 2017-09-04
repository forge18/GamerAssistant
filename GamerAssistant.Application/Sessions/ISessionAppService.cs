using System.Threading.Tasks;
using Abp.Application.Services;
using GamerAssistant.Sessions.Dto;

namespace GamerAssistant.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
