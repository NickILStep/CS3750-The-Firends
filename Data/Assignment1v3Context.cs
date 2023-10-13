using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Models;

namespace Assignment1v3.Data
{
    public class Assignment1v3Context : DbContext
    {
        public Assignment1v3Context (DbContextOptions<Assignment1v3Context> options)
            : base(options)
        {
        }

        public DbSet<Assignment1v3.Models.Login> Login { get; set; }

        public DbSet<Assignment1v3.Models.Course> Course { get; set; }

        public DbSet<Assignment1v3.Models.StudSched>? StudSched { get; set; }
    }
}
