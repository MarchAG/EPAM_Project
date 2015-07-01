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
    public class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("DefaultConnection")
        {

        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<UserProfile> Users { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

    }
}