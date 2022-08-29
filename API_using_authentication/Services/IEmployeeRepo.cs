using API_using_authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_using_authentication.Services
{
    public interface IEmployeeRepo
    {
         Task<IEnumerable<Employee>> GetEmployees();
         Task<Employee> GetEmployeeByID(int id);
         Task<Employee> GetEmployeeByName(string name);
        Task<Employee> CreateEmployee(Employee employee);
         Task<Employee> EditEmployee(Employee employee);
         Task<Employee> DeleteEmployee(int id);

       
    }
}
