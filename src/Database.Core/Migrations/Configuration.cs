using System;
using System.Data.Entity.Migrations;
using eSIS.Core.Entities;
using eSIS.Core.Entities.Infrastructure;

namespace eSIS.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SisContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SisContext context)
        {
            const string seedUser = "Seed";

            context.SchoolTypes.AddOrUpdate(p => p.Name,
                new SchoolType { Id = 1, SystemCode = "E", Name = "Elementary", AllowCustomGradingScales = true, AddUser = seedUser, AddDate = DateTime.Now },
                new SchoolType { Id = 2, SystemCode = "M", Name = "Middle", AllowCustomGradingScales = false, AddUser = seedUser, AddDate = DateTime.Now },
                new SchoolType { Id = 3, SystemCode = "H", Name = "High", AllowCustomGradingScales = false, AddUser = seedUser, AddDate = DateTime.Now });

            context.ApiClients.AddOrUpdate(p => p.Name,
                new ApiClient { Id = 1, SystemCode = null, Name = "eSISLocalDevelopmentUI", Token = "C880EAC5-A3E3-40F9-8083-140BF49A9B7C", IsActive = true, AddUser = seedUser, AddDate = DateTime.Now },
                new ApiClient { Id = 2, SystemCode = null, Name = "eSISLocalDevelopmentTaskPlayground", Token = "744F1564-859E-4A45-B387-AB95FC2C49FB", IsActive = true, AddUser = seedUser, AddDate = DateTime.Now });

            context.ResetQuestions.AddOrUpdate(p => p.SystemCode,
                new ResetQuestion { Id = 1, SystemCode = null, QuestionText = "First question", IsActive = true, AddUser = seedUser, AddDate = DateTime.Now },
                new ResetQuestion { Id = 2, SystemCode = null, QuestionText = "Second question", IsActive = true, AddUser = seedUser, AddDate = DateTime.Now });

            //context.Users.AddOrUpdate(p => p.SystemCode,
            //    new User
            //    {
            //        Id = 1,
            //        SystemCode = "jmadkins",
            //        FirstName = "Justin",
            //        MI = "M",
            //        LastName = "Adkins",
            //        EmailAddress = "justin.adkins@mythcoders.com",
            //        ResetQuestion1Id = 1,
            //        ResetQuestion1Answer = "test",
            //        ResetQuestion2Id = 2,
            //        ResetQuestion2Answer = "test",
            //        IsActive = true,
            //        Role = 1,
            //        AccountStatus = 1,
            //        AddUser = seedUser,
            //        AddDate = DateTime.Now
            //    });

            //context.Grades.AddOrUpdate(p => p.Name,
            //    new Grade { Id = 1, SystemCode = "K", Name = "Kingergarden", NextGradeId = null, PreviousGradeId = null, AddUser = seedUser, AddDate = DateTime.Now },
            //    new Grade { Id = 2, SystemCode = "1", Name = "1st Grade", NextGradeId = null, PreviousGradeId = 1, AddUser = seedUser, AddDate = DateTime.Now },
            //    new Grade { Id = 3, SystemCode = "2", Name = "2nd Grade", NextGradeId = null, PreviousGradeId = 2, AddUser = seedUser, AddDate = DateTime.Now },
            //    new Grade { Id = 4, SystemCode = "3", Name = "3rd Grade", NextGradeId = null, PreviousGradeId = 3, AddUser = seedUser, AddDate = DateTime.Now },
            //    new Grade { Id = 5, SystemCode = "4", Name = "4th Grade", NextGradeId = null, PreviousGradeId = 4, AddUser = seedUser, AddDate = DateTime.Now },
            //    new Grade { Id = 6, SystemCode = "5", Name = "5th Grade", NextGradeId = null, PreviousGradeId = 5, AddUser = seedUser, AddDate = DateTime.Now },
            //    new Grade { Id = 7, SystemCode = "6", Name = "6th Grade", NextGradeId = null, PreviousGradeId = 6, AddUser = seedUser, AddDate = DateTime.Now },
            //    new Grade { Id = 8, SystemCode = "7", Name = "7th Grade", NextGradeId = null, PreviousGradeId = 7, AddUser = seedUser, AddDate = DateTime.Now },
            //    new Grade { Id = 9, SystemCode = "8", Name = "8th Grade", NextGradeId = null, PreviousGradeId = 8, AddUser = seedUser, AddDate = DateTime.Now },
            //    new Grade { Id = 10, SystemCode = "9", Name = "9th Grade", NextGradeId = null, PreviousGradeId = 9, AddUser = seedUser, AddDate = DateTime.Now },
            //    new Grade { Id = 11, SystemCode = "10", Name = "10th Grade", NextGradeId = null, PreviousGradeId = 10, AddUser = seedUser, AddDate = DateTime.Now },
            //    new Grade { Id = 12, SystemCode = "11", Name = "11th Grade", NextGradeId = null, PreviousGradeId = 11, AddUser = seedUser, AddDate = DateTime.Now },
            //    new Grade { Id = 13, SystemCode = "12", Name = "12th Grade", NextGradeId = null, PreviousGradeId = 12, AddUser = seedUser, AddDate = DateTime.Now });
        }
    }
}
