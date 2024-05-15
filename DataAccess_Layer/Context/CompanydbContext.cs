using DataAccess_Layer.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess_Layer.Context
{
	public class CompanydbContext:IdentityDbContext<AppUser>
    {
        public CompanydbContext(DbContextOptions<CompanydbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasMany(d => d.employees).WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId).IsRequired(false);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
