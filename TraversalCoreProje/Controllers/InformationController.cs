using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Controllers
{
    public class InformationController : Controller
    {
        //https://localhost:5001/Information/Index/?culture=en
        public IActionResult Index()
        {
            return View();
        }
    }
}
