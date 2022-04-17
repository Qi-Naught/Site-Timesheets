using System;
using System.Threading.Tasks;
using Common.Data;
using Common.Models;
using TPFeuilleDeTemps_JeanGirard.Models.ViewModels;

namespace TPFeuilleDeTemps_JeanGirard.ApplicationLogic
{
    public class UserCreationViewModelProcessing
    {
        private readonly TimeSheetContext context;

        public UserCreationViewModelProcessing(TimeSheetContext context)
        {
            this.context = context;
        }

        public void LoadModel(UserCreationViewModel model)
        {
            model.Email = string.Empty;
            model.EmployeeNumber = 0;
            model.FirstName = string.Empty;
            model.LastName = string.Empty;
            model.PasswordConfirmation = string.Empty;
            model.Pwd = string.Empty;
        }

        public async Task SaveModel(UserCreationViewModel model)
        {
            User user = new()
            {
                Id = new Guid(),
                EmployeeNumber = model.EmployeeNumber,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Pwd = PwdHasherAndSalter.ComputeSha256SaltedHash(model.Email, model.Pwd)
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
    }
}