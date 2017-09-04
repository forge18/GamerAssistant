using System.Threading.Tasks;
using Abp.Application.Services;
using GamerAssistant.Configuration.Dto;

namespace GamerAssistant.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}