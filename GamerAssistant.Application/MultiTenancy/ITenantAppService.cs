using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GamerAssistant.MultiTenancy.Dto;

namespace GamerAssistant.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
