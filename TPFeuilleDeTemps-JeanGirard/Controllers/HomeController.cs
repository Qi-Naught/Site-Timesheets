using System.Diagnostics;
using Common.Data;
using Microsoft.AspNetCore.Mvc;
using TPFeuilleDeTemps_JeanGirard.Models.ViewModels;

namespace TPFeuilleDeTemps_JeanGirard.Controllers
{
    public class HomeController : Controller
    {
        private readonly TimeSheetContext context;

        public HomeController(TimeSheetContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}