using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_using_authentication.Services
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private DepartmentDbContext departmentDbContext;

        public DepartmentRepo(DepartmentDbContext departmentDbContext)
        {
            this.departmentDbContext = departmentDbContext;
        }
        public async Task<Department> CreateDepartment(Department department)
        {
            var result = await departmentDbContext.Departments.AddAsync(department);
            await departmentDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Department> DeleteDepartment(int id)
        {
            var result = await departmentDbContext.Departments.FirstOrDefaultAsync(model => model.DepartmentID == id);
            if(result!=null)
            {
                departmentDbContext.Departments.Remove(result);
                departmentDbContext.SaveChanges();

                
            }

            return result;
        }

        public async Task<Department> EditDepartment(Department department)
        {
            var result = await departmentDbContext.Departments.FirstOrDefaultAsync(model => model.DepartmentID == department.DepartmentID);

            if(result!=null)
            {
                result.DepartmentID = department.DepartmentID;
                result.DepartmentName = department.DepartmentName;
            }

            return result;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await departmentDbContext.Departments.FirstOrDefaultAsync(model => model.DepartmentID==id);
        }

        public async Task<Department> GetDepartmentByName(string name)
        {
            return await departmentDbContext.Departments.FirstOrDefaultAsync(model => model.DepartmentName == name);
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await departmentDbContext.Departments.ToListAsync();
        }
    }
}
