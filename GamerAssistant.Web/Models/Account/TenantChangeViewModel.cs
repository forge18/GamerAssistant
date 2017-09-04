using Abp.AutoMapper;
using GamerAssistant.Sessions.Dto;

namespace GamerAssistant.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}