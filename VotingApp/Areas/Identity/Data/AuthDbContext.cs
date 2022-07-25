using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VotingApp.Areas.Identity.Data;


namespace VotingApp.Data
{
    public class AuthDbContext : IdentityDbContext<VoteAppUser> 
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }
        public DbSet<VoteAppUser> VoteUser { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
          
            base.OnModelCreating(builder);
            builder.Entity<VoteAppUser>().ToTable("VoteUserr");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        
    }
}
