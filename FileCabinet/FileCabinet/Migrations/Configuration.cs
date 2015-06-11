namespace FileCabinet.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<FileCabinet.Models.MyDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FileCabinet.Models.MyDBContext context)
        {

            //if (!Roles.RoleExists("Administrator"))
            //    Roles.CreateRole("Administrator");

            //if (!WebSecurity.UserExists("lelong37"))
            //    WebSecurity.CreateUserAndAccount(
            //        "lelong37",
            //        "password",
            //        new { Mobile = "+19725000000", IsSmsVerified = false });

            //if (!Roles.GetRolesForUser("lelong37").Contains("Administrator"))
            //    Roles.AddUsersToRoles(new[] { "lelong37" }, new[] { "Administrator" });
        }
    }
}
