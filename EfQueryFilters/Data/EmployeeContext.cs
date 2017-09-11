using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EfQueryFilters.Data
{
    public class EmployeeContext : DbContext
    {
        private int _companyId;
        
        public EmployeeContext(int companyId, DbContextOptions options) : base(options)
        {
            _companyId = companyId;
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasQueryFilter(e => !e.IsDeleted 
                                  && e.CompanyId == _companyId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
