using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrudAngularWebApi.Data;
using CrudAngularWebApi.Models;

namespace CrudAngularWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeData _employeeData;

        public EmployeeController(EmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        [HttpGet]
        
        public async Task<IActionResult> List()
        {
            List<Employee> employees = await _employeeData.List();
            return StatusCode(StatusCodes.Status200OK, employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Employee employee = await _employeeData.Get(id);
            return StatusCode(StatusCodes.Status200OK, employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            bool response = await _employeeData.Create(employee);
            return StatusCode(StatusCodes.Status200OK, new {isSuccess = response});
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Employee employee)
        {
            bool response = await _employeeData.Update(employee);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = response });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool response = await _employeeData.Delete(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = response });
        }
    }
}
