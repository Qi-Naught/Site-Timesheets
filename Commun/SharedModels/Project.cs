using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Project
    {
        [Key] public Guid Id { get; set; }

        public int ProjectNumber { get; set; }

        public string Name { get; set; }
    }
}