using GamerAssistant.Sources;
using System.Web.Mvc;

namespace GamerAssistant.Web.Controllers
{
    public class GameLibraryController : Controller
    {
        private readonly SourceAppService _sourceService;

        public GameLibraryController(
            SourceAppService sourceService
        )
        {
            _sourceService = sourceService;
        }


        // GET: GameLibrary
        public ActionResult Index()
        {
            return View();
        }

        public void Test()
        {
            var userId = 3;
            var library = _sourceService.GetBggCollectionByUserId(userId);
            var gameDetails = _sourceService.GetGameDetailFromBggByGameId("215");
            var searchResults = _sourceService.SearchBggGames("Ark");
            var test = 0;
        }
    }
}