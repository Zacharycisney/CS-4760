using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CS4760Group1.Models;

namespace CS4760Group1.Data
{
    public class CS4760Group1Context : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public CS4760Group1Context(DbContextOptions<CS4760Group1Context> options)
            : base(options)
        {
        }

        public DbSet<College> College { get; set; } = default!;
        public DbSet<Department> Department { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure Identity schema is configured

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(
                    v => v.ToString(),   // Convert the enum to string when saving to the database
                    v => (Role)Enum.Parse(typeof(Role), v)  // Convert the string back to the enum when reading from the database
                );
        }
    }
}