using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TPFeuilleDeTemps_JeanGirard.ApplicationLogic
{
    public class IsValidEmail : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Please enter a valid email address.";
        }

        protected override ValidationResult IsValid(object objValue, ValidationContext validationContext)
        {
            string email = objValue as string;


            if (string.IsNullOrWhiteSpace(email))
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));


            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    IdnMapping idn = new();

                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            catch (ArgumentException)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s\W]+@[^@\s\W]+\.[^@\s\W]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250))
                    ? ValidationResult.Success
                    : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            catch (RegexMatchTimeoutException)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
        }
    }
}