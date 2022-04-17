using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TPFeuilleDeTemps_JeanGirard.ApplicationLogic;

namespace TPFeuilleDeTemps_JeanGirard.Models.ViewModels
{
    public class UserCreationViewModel
    {
        [Required]
        [DisplayName("Employee number")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Employee number.")]
        public int EmployeeNumber { get; set; }

        [Required] [DisplayName("First name")] public string FirstName { get; set; }
        [Required] [DisplayName("Last name")] public string LastName { get; set; }

        [Required]
        [IsValidEmail]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "The password must be at least 8 characters long.")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]

        public string Pwd { get; set; }

        [Required]
        [Compare("Pwd", ErrorMessage = "The password does not match the confirm your password field.")]
        [DisplayName("Confirm your password")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }
}