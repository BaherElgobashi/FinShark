using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }


        public DbSet<Stock>Stocks {get;set;} 
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Portfolio> Portfolios {get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Make Composite key from AppUserId and StockId which is declared in the Portfolio Module.
            builder.Entity<Portfolio>(x=>x.HasKey(k=>new{k.AppUserId,k.StockId}));

            // Make Relationship with AppUser.
            builder.Entity<Portfolio>()
            .HasOne(a=>a.AppUser)
            .WithMany(a => a.Portfolios)
            .HasForeignKey(a=> a.AppUserId);

            // Make Relationship with AppUser.
            builder.Entity<Portfolio>()
            .HasOne(s=>s.Stock)
            .WithMany(s=>s.Portfolios)
            .HasForeignKey(s=>s.StockId);
            

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    
                    Name = "Admin",
                    NormalizedName = "Admin"
                },
                new IdentityRole
                {
                    
                    Name = "User",
                    NormalizedName = "User"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

        
}
}