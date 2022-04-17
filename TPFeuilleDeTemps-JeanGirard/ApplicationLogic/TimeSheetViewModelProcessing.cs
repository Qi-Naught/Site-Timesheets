using System;
using System.Threading.Tasks;
using Common.Data;
using Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPFeuilleDeTemps_JeanGirard.Models.ViewModels;

namespace TPFeuilleDeTemps_JeanGirard.ApplicationLogic
{
    public class TimeSheetViewModelProcessing
    {
        private readonly TimeSheetContext context;

        public TimeSheetViewModelProcessing(TimeSheetContext context)
        {
            this.context = context;
        }

        public void LoadModel(TimeSheetViewModel model)
        {
            model.Projects = new SelectList(context.Projects, "Id", "Name");
            model.StartDateTime = new DateTime();
            model.EndDateTime = new DateTime();
        }

        public async Task SaveModel(TimeSheetViewModel model)
        {
            TimeSheet timeEntry = new()
            {
                Id = Guid.NewGuid(),
                User = context.Users.Find(model.CurrentUser),
                Project = context.Projects.Find(model.SelectedProject),
                StartDateTime = model.StartDateTime,
                EndDateTime =
                    model.EndDateTime
            };
            context.TimeSheets.Add(timeEntry);
            await context.SaveChangesAsync();
        }
    }
}