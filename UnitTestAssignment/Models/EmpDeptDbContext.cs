using Microsoft.EntityFrameworkCore;

namespace UnitTestAssignment.Models
{
    public class EmpDeptDbContext : DbContext
    {
        public EmpDeptDbContext(DbContextOptions<EmpDeptDbContext> options) : base(options)
        { 
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
