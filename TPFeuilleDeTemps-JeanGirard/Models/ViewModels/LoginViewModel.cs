using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPFeuilleDeTemps_JeanGirard.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [DisplayName("Enter your email : ")]

        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Password)]
        [DisplayName("Enter your password : ")]
        public string Pwd { get; set; }

        public bool RememberMe { get; set; }
    }
}