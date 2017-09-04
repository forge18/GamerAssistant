using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GamerAssistant.Roles.Dto;
using GamerAssistant.Users.Dto;

namespace GamerAssistant.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}