using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_using_authentication.Services
{
    public interface IDepartmentRepo
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartmentById(int id);
        Task<Department> GetDepartmentByName(string name);
        Task<Department> CreateDepartment(Department department);

        Task<Department> EditDepartment(Department department);

        Task<Department> DeleteDepartment(int id);

    }
}
