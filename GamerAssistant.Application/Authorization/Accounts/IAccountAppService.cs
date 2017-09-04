using System.Threading.Tasks;
using Abp.Application.Services;
using GamerAssistant.Authorization.Accounts.Dto;

namespace GamerAssistant.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
