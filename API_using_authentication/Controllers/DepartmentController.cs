using API_using_authentication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_using_authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartmentRepo departmentRepo;
        public DepartmentController(IDepartmentRepo departmentRepo)
        {
            this.departmentRepo = departmentRepo;
        }

        [HttpGet]

        public async Task<ActionResult> GetDepartments()
        {
            return Ok(await departmentRepo.GetDepartments());
        }

        [HttpGet("{name}")]

        public async Task<ActionResult>GetDepartmentname(string name)
        {
            return Ok(await departmentRepo.GetDepartmentByName(name));
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult> GetDepartmentID(int id)
        {
            return Ok(await departmentRepo.GetDepartmentById(id));
        }

        [HttpPost]

        public async Task<ActionResult<Department>> CreateDepartment(Department department)
        {
            var CreatedDepartment = await departmentRepo.CreateDepartment(department);
          

           return CreatedAtAction(nameof(GetDepartmentID), new { id = CreatedDepartment.DepartmentID }, CreatedDepartment);

        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult<Department>> EditDepartment(int id, Department department)
        {
            var updatedDepartment = await departmentRepo.GetDepartmentById(id);

            return await departmentRepo.EditDepartment(department);

        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var deleteDepartment = await departmentRepo.GetDepartmentById(id);

            return await departmentRepo.DeleteDepartment(id);

        }

    }
}
