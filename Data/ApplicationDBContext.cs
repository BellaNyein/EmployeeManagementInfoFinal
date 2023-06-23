using EmployeeManagementInfo.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementInfo.Data
{
    public class ApplicationDBContext : DbContext

    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Position> Position { get; set; }
    }
}
