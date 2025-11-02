using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
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

        [HttpGet]
        public ActionResult<List<EmployeeDto>> GetAllEmployees()
        {
            return Ok(_employeeService.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeDto> GetById([FromRoute] int id)
        {
            return Ok(_employeeService.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<EmployeeDto> Create([FromBody] CreationEmployeeDto creationEmployeeDto)
        {
            return Ok(_employeeService.Create(creationEmployeeDto));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Update([FromRoute] int id, [FromBody] CreationEmployeeDto creationEmployeeDto)
        {
            _employeeService.Update(id, creationEmployeeDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            _employeeService.Delete(id);
            return NoContent();
        }
    }
}
