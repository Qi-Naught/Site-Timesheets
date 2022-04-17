using System;
using System.ComponentModel.DataAnnotations;

namespace TPFeuilleDeTemps_JeanGirard.ApplicationLogic
{
    public class DateIsASunday : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Please enter a date that is a Sunday.";
        }

        protected override ValidationResult IsValid(object objValue,
                                                    ValidationContext validationContext)
        {
            DateTime selectedDateTime = objValue as DateTime? ?? new DateTime();

            return selectedDateTime.DayOfWeek != DayOfWeek.Sunday
                ? new ValidationResult(FormatErrorMessage(validationContext.DisplayName))
                : ValidationResult.Success;
        }
    }
}