using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class User
    {
        [Key] public Guid Id { get; set; }
        public int EmployeeNumber { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Pwd { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}