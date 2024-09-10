using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CS4760Group1.Models;

namespace CS4760Group1.Data
{
    public class CS4760Group1Context : DbContext
    {
        public CS4760Group1Context (DbContextOptions<CS4760Group1Context> options)
            : base(options)
        {
        }

        public DbSet<CS4760Group1.Models.College> College { get; set; } = default!;
        public DbSet<CS4760Group1.Models.Department> Department { get; set; } = default!;
        public DbSet<CS4760Group1.Models.User> User { get; set; } = default!;
    }
}
