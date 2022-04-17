using System;
using System.ComponentModel.DataAnnotations;

namespace TPFeuilleDeTemps_JeanGirard.ApplicationLogic
{
    public class DateNotInFuture : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Please enter a value that is not a future date.";
        }

        protected override ValidationResult IsValid(object objValue,
                                                    ValidationContext validationContext)
        {
            DateTime selectedDateTime = objValue as DateTime? ?? new DateTime();

            if (selectedDateTime > DateTime.Now)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            return ValidationResult.Success;
        }
    }
}