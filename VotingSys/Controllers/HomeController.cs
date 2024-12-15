using System.Web.Mvc;
using VotingSys.Models;

namespace VotingSys.Controllers
{
    public class HomeController : Controller
    {
        private DataContext context = new DataContext();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}