using System;
using System.Diagnostics;
using Common.Data;
using Common.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPFeuilleDeTemps_JeanGirard.ApplicationLogic;

namespace TPFeuilleDeTemps_JeanGirard.Tests
{
    [TestClass]
    public class DataAccessTest
    {
        private readonly TimeSheetContext context;

        public DataAccessTest()
        {
            DbContextOptionsBuilder<TimeSheetContext> opt = new();
            DefaultConnectionContext defaultConnection = new();
            opt.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=TPFeuilleDeTemps;Trusted_Connection=True;MultipleActiveResultSets=true");
            TimeSheetContext context = new(opt.Options);
            this.context = context;
        }

        [TestMethod]
        public void FullDataAccesTest()
        {
            Guid userId = Guid.NewGuid();
            Guid projectId = Guid.NewGuid();
            Guid timeSheetId = Guid.NewGuid();

            CreationTest(userId, projectId, timeSheetId);
            LoadingTest(userId, projectId, timeSheetId);
            UpdateTest(userId, projectId, timeSheetId);
            DeleteTest(userId, projectId, timeSheetId);
        }

        private void CreationTest(Guid userId, Guid projectId, Guid timeSheetId)
        {
            context.Users.Add(new User
            {
                Id = userId,
                EmployeeNumber = 6,
                Email = "banana@dk.com",
                FirstName = "Didy",
                LastName = "Kong",
                Pwd = PwdHasherAndSalter.ComputeSha256SaltedHash("banana@dk.com", "B4n4n3")
            });
            context.Projects.Add(new Project { Id = projectId, Name = "JungleGame", ProjectNumber = 3 });
            context.Add(new TimeSheet
            {
                Id = timeSheetId,
                Project = context.Projects.Find(projectId),
                User = context.Users.Find(userId),
                StartDateTime = new DateTime(2020, 12, 10, 8, 4, 2),
                EndDateTime = new DateTime(2020, 12, 10, 12, 5, 3)
            });
            context.SaveChanges();
        }

        private void LoadingTest(Guid userId, Guid projectId, Guid timeSheetId)
        {
            Debug.Assert(context.Users.Find(userId) != null);
            Debug.Assert(context.Projects.Find(projectId) != null);
            Debug.Assert(context.TimeSheets.Find(timeSheetId) != null);
        }

        private void UpdateTest(Guid userId, Guid projectId, Guid timeSheetId)
        {
            Project projectToChange = context.Projects.Find(projectId);
            projectToChange.Name = "Bouette";
            TimeSheet timeSheetToChange = context.TimeSheets.Find(timeSheetId);
            timeSheetToChange.Project = projectToChange;
            User userToChange = context.Users.Find(userId);
            userToChange.Email = "nouveaucourriel@ya.ca";
            context.SaveChanges();
            Debug.Assert(context.Users.Find(userId).Email == "nouveaucourriel@ya.ca");
            Debug.Assert(context.TimeSheets.Find(timeSheetId).Project.Id == projectToChange.Id);
            Debug.Assert(context.Projects.Find(projectId).Name == "Bouette");
        }

        private void DeleteTest(Guid userId, Guid projectId, Guid timeEntryId)
        {
            context.Users.Remove(context.Users.Find(userId));
            context.Projects.Remove(context.Projects.Find(projectId));
            context.TimeSheets.Remove(context.TimeSheets.Find(timeEntryId));
            context.SaveChanges();

            Debug.Assert(context.Users.Find(userId) == null);
            Debug.Assert(context.Projects.Find(projectId) == null);
            Debug.Assert(context.TimeSheets.Find(timeEntryId) == null);
        }

        /*   public void CreerTuplesInitiaux()
           {
               Voir Data/DbInitializer.cs
           }
        */
    }
}