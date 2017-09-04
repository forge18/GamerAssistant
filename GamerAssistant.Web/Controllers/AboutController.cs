using System.Web.Mvc;

namespace GamerAssistant.Web.Controllers
{
    public class AboutController : GamerAssistantControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}