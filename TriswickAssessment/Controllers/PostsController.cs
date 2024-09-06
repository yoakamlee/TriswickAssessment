using Microsoft.AspNetCore.Mvc;

namespace TriswickAssessment.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
