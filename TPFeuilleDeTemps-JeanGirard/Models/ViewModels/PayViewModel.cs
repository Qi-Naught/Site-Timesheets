using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPFeuilleDeTemps_JeanGirard.ApplicationLogic;

namespace TPFeuilleDeTemps_JeanGirard.Models.ViewModels
{
    public class PayViewModel
    {
        [Required] public Guid SelectedUser { get; set; }
        [DisplayName("Select a User")] public SelectList Users { get; set; }

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