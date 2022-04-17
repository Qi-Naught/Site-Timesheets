using Common.Data;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPFeuilleDeTemps_JeanGirard.ApplicationLogic;
using TPFeuilleDeTemps_JeanGirard.Models.ViewModels;

namespace TPFeuilleDeTemps_JeanGirard.Controllers
{
    public class PayController : Controller
    {
        private readonly TimeSheetContext context;

        public PayController(TimeSheetContext context)
        {
            this.context = context;
        }

        [Authorize]
        public IActionResult RequestForm()
        {
            PayViewModel model = new();
            PayViewModelProcessing processing = new(context);
            processing.LoadModel(model);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestForm(PayViewModel payViewModel, string submitButton)
        {
            PayViewModelProcessing processing = new(context);
            if (ModelState.IsValid)
            {
                PayRequestDTO model = new()
                {
                    Id = payViewModel.SelectedUser, StartDateTime = payViewModel.StartDateTime
                };
                ViewBag.submitButton = submitButton;
                ViewBag.nbDays = payViewModel.EndDateTime.Subtract(payViewModel.StartDateTime).Days + 1;
                return View("Index", model);
                ;
            }

            processing.LoadModel(payViewModel);

            TempData["UserMessage"] = new ValidationMessageForView
            {
                CssClassName = "alert-danger", Title = "Failure!",
                Message = "The request for informations was not sent!"
            };

            return View(payViewModel);
        }
    }
}