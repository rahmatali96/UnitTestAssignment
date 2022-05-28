using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnitTestAssignment.Models;
using UnitTestAssignment.Services;

namespace UnitTestAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public EmployeesController()
        {
        }

        //Get All Employee
        [HttpGet("GetAllEmployee")]
        public async Task<ActionResult<Employee>> GetAllEmployee()
        {
            var data = await _employeeService.GetAllEmployeeAsync();
            return Ok(data);
        }

        //Get Employee By Email
        [HttpGet("GetEmployeeByEmail")]
        public async Task<ActionResult> GetEmployeeByEmail(string email)
        {
            var result = await _employeeService.GetEmployeeByEmailAsync(email);
            if (result == null)
            {
                return NotFound();
            }
            else
                return Ok(result);
        }

        //Get Employee By DeptId
        [HttpGet("GetEmployeeByDeptId")]
        public async Task<ActionResult> GetEmployeeByDeptId(int deptid)
        {
            var result = await _employeeService.GetEmployeeByDeptIdAsync(deptid);
            if (result == null)
            {
                return NotFound();
            }
            else
                return Ok(result);
        }

        //Post Employee
        [HttpPost("PostEmployee")]
        public async Task<ActionResult<Employee[]>> PostEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            var exist = await _employeeService.GetEmployeeByEmailAsync(employee.Email);
            if (exist != null)
            {
                return Conflict(employee);
            }

            var existDept = await _employeeService.GetDepartmentByDeptIdAsync(employee.DeptId);
            var dept = employee.DeptId;
            if (dept.Equals(existDept))
            {
                _employeeService.Add(employee);
                await _employeeService.SaveChangesAsync();
                return Ok(employee);
            }
            return NotFound("Department not exist");
            
                
        }

    }
}
