using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using GamerAssistant.Configuration.Dto;

namespace GamerAssistant.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : GamerAssistantAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
