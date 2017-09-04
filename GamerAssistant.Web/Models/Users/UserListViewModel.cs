using System.Collections.Generic;
using GamerAssistant.Roles.Dto;
using GamerAssistant.Users.Dto;

namespace GamerAssistant.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}