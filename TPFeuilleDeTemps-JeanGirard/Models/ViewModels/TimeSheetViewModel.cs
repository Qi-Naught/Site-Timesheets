using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPFeuilleDeTemps_JeanGirard.ApplicationLogic;

namespace TPFeuilleDeTemps_JeanGirard.Models.ViewModels
{
    public class TimeSheetViewModel
    {
        [Required] public Guid CurrentUser { get; set; }

        [Required] public Guid SelectedProject { get; set; }

        [DisplayName("Select a project")] public SelectList Projects { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DateNotInFuture]
        [DisplayName("Start Date")]
        public DateTime StartDateTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DateNotInFuture]
        [DisplayName("End Date")]
        public DateTime EndDateTime { get; set; }
    }
}