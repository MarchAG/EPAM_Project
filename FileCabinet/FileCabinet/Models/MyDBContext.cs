using FileCabinet.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace FileCabinet.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("DefaultConnection")
        {

        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<UserProfile> Users { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyDBContext, Configuration>());
        //    base.OnModelCreating(modelBuilder);
        //    try
        //    {
        //        using (var context = new MyDBContext())
        //        {
        //            if (!context.Database.Exists())
        //            {
        //                // Create the SimpleMembership database without Entity Framework migration schema
        //                ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
        //            }
        //        }

        //        WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserProfileId", "Username", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
        //    }
        //}


    }
}