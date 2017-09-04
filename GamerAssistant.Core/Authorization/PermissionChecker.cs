using Abp.Authorization;
using GamerAssistant.Authorization.Roles;
using GamerAssistant.Authorization.Users;

namespace GamerAssistant.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
