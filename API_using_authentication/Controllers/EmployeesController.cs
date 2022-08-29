using API_using_authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_using_authentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeRepo employeeRepo;

        public EmployeesController(IEmployeeRepo employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }

        [HttpGet]

        public async Task<ActionResult> GetEmployees()
        {
            
            return Ok(await employeeRepo.GetEmployees());
        }

        [HttpGet ("{id:int}")]

        public async Task<ActionResult>GetEmployeeByID(int id)
        {
           
            return Ok(await employeeRepo.GetEmployeeByID(id));
        }

        [HttpGet("{string}")]
        public async Task<ActionResult>GetEmployeeByName(string name)
        {
            return Ok(await employeeRepo.GetEmployeeByName(name));
        }

        [HttpPost]

        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            var CreatedEmployee = await employeeRepo.CreateEmployee(employee);

            return CreatedAtAction(nameof(GetEmployeeByID), new { id = CreatedEmployee.EmployeeID }, CreatedEmployee);

        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult<Employee>> EditEmployee(int id, Employee employee)
        {
            var updatedEmployee = await employeeRepo.GetEmployeeByID(id);

            return await employeeRepo.EditEmployee(employee);

        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var deleteEmployee = await employeeRepo.GetEmployeeByID(id);

            return await employeeRepo.DeleteEmployee(id);

        }

    }
}
