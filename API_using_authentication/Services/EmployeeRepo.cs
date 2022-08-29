using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_using_authentication.Services
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private DepartmentDbContext departmentDbContext;
        public EmployeeRepo(DepartmentDbContext departmentDbContext)
        {
            this.departmentDbContext = departmentDbContext;
        }


       
        public async Task<Employee> CreateEmployee(Employee employee)
        {
            if (employee.Department != null)
            {
                departmentDbContext.Entry(employee.Department).State = EntityState.Unchanged;
            }

            var result = await departmentDbContext.Employees.AddAsync(employee);
            await departmentDbContext.SaveChangesAsync();
            return result.Entity;

          
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            var result = await departmentDbContext.Employees.FirstOrDefaultAsync(model => model.EmployeeID == id);

            if (result != null)
            {
                departmentDbContext.Employees.Remove(result);
                await departmentDbContext.SaveChangesAsync();

            }
            return result;
        }

        public async Task<Employee> EditEmployee(Employee employee)
        {
            var result = await departmentDbContext.Employees.FirstOrDefaultAsync(model => model.EmployeeID == employee.EmployeeID);

            if (result != null)
            {
                result.EmployeeID = employee.EmployeeID;
                result.EmployeeName = employee.EmployeeName;
                result.Email = employee.Email;
                
                result.DepartmentID = employee.DepartmentID;

                await departmentDbContext.SaveChangesAsync();

            }
            return result;
        }

        public async Task<Employee> GetEmployeeByID(int id)
        {
            return await departmentDbContext.Employees.Include(model => model.Department).FirstOrDefaultAsync(model => model.EmployeeID == id);
        }

       

        public async Task<Employee> GetEmployeeByName(string name)
        {
            return await departmentDbContext.Employees.FirstOrDefaultAsync(model => model.EmployeeName == name);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
               return await departmentDbContext.Employees.ToListAsync();  
        }

    }
}
