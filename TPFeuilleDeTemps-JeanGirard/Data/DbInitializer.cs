using System;
using System.Linq;
using Common.Data;
using Common.Models;
using TPFeuilleDeTemps_JeanGirard.ApplicationLogic;

namespace TPFeuilleDeTemps_JeanGirard.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TimeSheetContext context)
        {
            if (context.TimeSheets.Any()) return; // DB has been seeded

            User[] users =
            {
                new()
                {
                    FirstName = "john", LastName = "joe", Email = "eksjfjka@hotmail.com", EmployeeNumber = 1,
                    Pwd = PwdHasherAndSalter.ComputeSha256SaltedHash("eksjfjka@hotmail.com", "23423")
                },
                new()
                {
                    FirstName = "bob", LastName = "ba", Email = "bobe@hotmail.ca", EmployeeNumber = 2,
                    Pwd = PwdHasherAndSalter.ComputeSha256SaltedHash("bobe@hotmail.ca", "eeew")
                },
                new()
                {
                    FirstName = "pere", LastName = "nouel", Email = "santa@gmail.com", EmployeeNumber = 3,
                    Pwd = PwdHasherAndSalter.ComputeSha256SaltedHash("santa@gmail.com", "ej@e5")
                },
                new()
                {
                    FirstName = "mere", LastName = "theresa", Email = "mom@gmail.com", EmployeeNumber = 4,
                    Pwd = PwdHasherAndSalter.ComputeSha256SaltedHash("mom@gmail.com", "gentillemedame")
                },
                new()
                {
                    FirstName = "ye", LastName = "lad", Email = "ye@gmail.com", EmployeeNumber = 5,
                    Pwd = PwdHasherAndSalter.ComputeSha256SaltedHash("ye@gmail.com", "yeye")
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            Project[] projects =
            {
                new() { ProjectNumber = 120, Name = "api" },
                new() { ProjectNumber = 121, Name = "serveur" },
                new() { ProjectNumber = 122, Name = "client" },
                new() { ProjectNumber = 123, Name = "bd" },
                new() { ProjectNumber = 124, Name = "cloud" }
            };

            context.Projects.AddRange(projects);
            context.SaveChanges();

            TimeSheet[] timeEntries =
            {
                new()
                {
                    User = users[0], Project = projects[0], StartDateTime = DateTime.Now, EndDateTime = DateTime.Now
                },
                new()
                {
                    User = users[1], Project = projects[1], StartDateTime = DateTime.Now, EndDateTime = DateTime.Now
                },
                new()
                {
                    User = users[2], Project = projects[2], StartDateTime = DateTime.Now, EndDateTime = DateTime.Now
                },
                new()
                {
                    User = users[3], Project = projects[3], StartDateTime = DateTime.Now, EndDateTime = DateTime.Now
                },
                new()
                {
                    User = users[4], Project = projects[4], StartDateTime = DateTime.Now, EndDateTime = DateTime.Now
                }
            };

            context.TimeSheets.AddRange(timeEntries);
            context.SaveChanges();
        }
    }
}