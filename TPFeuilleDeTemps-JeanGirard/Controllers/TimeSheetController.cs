using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPFeuilleDeTemps_JeanGirard.ApplicationLogic;
using TPFeuilleDeTemps_JeanGirard.Models.ViewModels;

namespace TPFeuilleDeTemps_JeanGirard.Controllers
{
    public class TimeSheetController : Controller
    {
        private readonly TimeSheetContext context;

        public TimeSheetController(TimeSheetContext context)
        {
            this.context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(context.TimeSheets.Include(c => c.Project).Include(c => c.User).ToList());
        }

        [Authorize]
        public IActionResult Create()
        {
            TimeSheetViewModel model = new();

            TimeSheetViewModelProcessing processing = new(context);
            processing.LoadModel(model);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("SelectedProject,StartDateTime,EndDateTime")]
            TimeSheetViewModel timeSheetViewModel)
        {
            TimeSheetViewModelProcessing processing = new(context);
            if (timeSheetViewModel.StartDateTime >= timeSheetViewModel.EndDateTime)
                ModelState.AddModelError(string.Empty, "The Start Date is bigger or equal than the End Date");

            if (ModelState.IsValid)
            {
                Guid userId = Guid.Parse(User.Claims.First(c => c.Type == "Id").Value);
                timeSheetViewModel.CurrentUser = (await context.Users.FindAsync(userId)).Id;
                await processing.SaveModel(timeSheetViewModel);
                timeSheetViewModel = new TimeSheetViewModel();
                TempData["UserMessage"] = new ValidationMessageForView { Message = "The time sheet was added!" };

                processing.LoadModel(timeSheetViewModel);

                return View("Create", timeSheetViewModel);
            }

            processing.LoadModel(timeSheetViewModel);

            TempData["UserMessage"] = new ValidationMessageForView
                { CssClassName = "alert-danger", Title = "Failure!", Message = "The time sheet was not added!" };

            return View(timeSheetViewModel);
        }
    }
}