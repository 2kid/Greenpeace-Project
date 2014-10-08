namespace GreenpeaceWeatherAdvisory.Migrations
{
    using GreenpeaceWeatherAdvisory.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GreenpeaceWeatherAdvisory.Models.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GreenpeaceWeatherAdvisory.Models.DBContext context)
        {
            bool success = false;

            var idManager = new IdentityManager();
            success = idManager.CreateRole("SuperAdmin", "SuperAdministrator");
            if (!success == true) return;

            success = idManager.CreateRole("Admin", "Administrator");
            if (!success == true) return;

            success = idManager.CreateRole("Staff", "Staff");
            if (!success) return;

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager) { AllowOnlyAlphanumericUserNames = false };

            string password = "Testing!1";
            //Create User=Admin with password=12345678
            var newUser = new ApplicationUser()
            {
                UserName = "sa",
                Email = "admin@yahoo.com"
             
            };
            var adminresult = UserManager.Create(newUser, password);
            //            Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                IdentityResult result = UserManager.AddToRole(newUser.Id, "SuperAdmin");
                result = UserManager.AddToRole(newUser.Id, "Admin");
                result = UserManager.AddToRole(newUser.Id, "Staff");

            }
            base.Seed(context);
        }
    }
}
