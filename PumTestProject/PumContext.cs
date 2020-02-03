using PumTestProject.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject
{
    public class PumContext : DbContext
    {

        public PumContext() : base(nameOrConnectionString: "Default") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }

    }
}
