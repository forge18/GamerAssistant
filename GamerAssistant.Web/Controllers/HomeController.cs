using Abp.Web.Mvc.Authorization;
using System.Web.Mvc;

namespace GamerAssistant.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : GamerAssistantControllerBase
    {
        public ActionResult Index()
        {
            //Get the current user id
            var userId = (int)AbpSession.UserId;

            ViewBag.UserId = userId;

            return View("Index");
        }
    }
}