using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_using_authentication
{
    public class DepartmentDbContext:DbContext
    {
        public DepartmentDbContext(DbContextOptions<DepartmentDbContext>options):base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Users> User { get; set; }
    }
}
