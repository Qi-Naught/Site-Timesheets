using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPFeuilleDeTemps_JeanGirard.ApplicationLogic;

namespace TPFeuilleDeTemps_JeanGirard.Tests
{
    [TestClass]
    public class EmailValidationTest
    {
        [TestMethod]
        public void Test()
        {
            IsValidEmail email = new();
            ValidationContext context = new(string.Empty);
            Debug.Assert(email.GetValidationResult("example@example.com", context) == ValidationResult.Success);
            Debug.Assert(email.GetValidationResult("example@example", context) != ValidationResult.Success);
            Debug.Assert(email.GetValidationResult("example.com", context) != ValidationResult.Success);
            Debug.Assert(email.GetValidationResult("example ;delete * from user ;@example.com", context) !=
                         ValidationResult.Success);
            Debug.Assert(email.GetValidationResult("exam&ple@example.com", context) != ValidationResult.Success);
        }
    }
}