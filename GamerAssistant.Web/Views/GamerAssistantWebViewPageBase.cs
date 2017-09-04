using Abp.Web.Mvc.Views;

namespace GamerAssistant.Web.Views
{
    public abstract class GamerAssistantWebViewPageBase : GamerAssistantWebViewPageBase<dynamic>
    {

    }

    public abstract class GamerAssistantWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected GamerAssistantWebViewPageBase()
        {
            LocalizationSourceName = GamerAssistantConsts.LocalizationSourceName;
        }
    }
}