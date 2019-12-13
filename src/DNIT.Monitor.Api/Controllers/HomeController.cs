using Microsoft.AspNetCore.Mvc;

namespace DNIT.Monitor.Api.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return Redirect("swagger");
        }
    }
}