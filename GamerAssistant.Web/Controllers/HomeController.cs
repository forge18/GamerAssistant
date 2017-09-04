using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace GamerAssistant.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : GamerAssistantControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}