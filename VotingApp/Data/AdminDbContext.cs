using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Areas.Identity.Data;
using VotingApp.Models;

namespace VotingApp.Data
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options)
          : base(options)
        {
        }
        public DbSet<AdminUser> Admin { get; set; }
        public DbSet<TVshow> TvShow { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AdminUser>().ToTable("Admintable");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

       
    }
}

