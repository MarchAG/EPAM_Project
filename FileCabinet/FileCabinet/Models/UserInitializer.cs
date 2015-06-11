using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace FileCabinet.Models
{
    public class UsersInitializer : DropCreateDatabaseAlways<MyDBContext>
    {
        protected override void Seed(MyDBContext context)
        {
            if (!Roles.RoleExists("Admin"))
                Roles.CreateRole("Admin");

            if (!WebSecurity.UserExists("Anton"))
                WebSecurity.CreateUserAndAccount(
                    "Anton",
                    "145236",
                    new { Email = "+aaa@aa.aa"});

            if (!Roles.GetRolesForUser("Anton").Contains("Admin"))
                Roles.AddUsersToRoles(new[] { "Anton" }, new[] { "Admin" });
            //var user = context.Users.Add(new UserProfile()
            //{
            //    Username = "baton",
            //    Email = "SomeEm",
            //});
            //context.Articles.Add(new Article()
            //{
            //    User = user,
            //    PathToContent = "alala",
            //    ContentType = ContentFileType.Text,
            //    DateOfPublication = "aasdas",
            //    Title = "sos"
            //});
            //context.Articles.Add(new Article()
            //{
            //    User = user,
            //    PathToContent = "dsadsa",
            //    ContentType = ContentFileType.Video,
            //    DateOfPublication = "dsa",
            //    Title = "oso"
            //});
            //base.Seed(context);
        }
    }
}