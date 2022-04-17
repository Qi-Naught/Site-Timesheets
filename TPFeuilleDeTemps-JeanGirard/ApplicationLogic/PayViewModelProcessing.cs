using System;
using Common.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPFeuilleDeTemps_JeanGirard.Models.ViewModels;

namespace TPFeuilleDeTemps_JeanGirard.ApplicationLogic
{
    public class PayViewModelProcessing
    {
        private readonly TimeSheetContext context;

        public PayViewModelProcessing(TimeSheetContext context)
        {
            this.context = context;
        }

        public void LoadModel(PayViewModel model)
        {
            model.Users = new SelectList(context.Users, "Id", "Email");
            model.StartDateTime = new DateTime();
        }
    }
}