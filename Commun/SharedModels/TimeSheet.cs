using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class TimeSheet
    {
        [Key] public Guid Id { get; set; }

        public User User { get; set; }

        public Project Project { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}