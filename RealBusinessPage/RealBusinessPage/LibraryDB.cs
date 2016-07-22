using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using RealBusinessPage.Models;

namespace RealBusinessPage
{
    public class LibraryDB : IdentityDbContext<IdentityUser>
    {
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Loans> Loans { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public LibraryDB()
            : base("LibraryDB")
        {

        }
    }
}