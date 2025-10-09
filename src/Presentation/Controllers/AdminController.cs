using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet]
        public ActionResult<List<AdminDto>> GetAllAdmins()
        {
            return Ok(_adminService.GetAllAdmins());
        }
        [HttpGet("{id}")]
        public ActionResult<AdminDto> GetById([FromRoute] int id)
        {
            return Ok(_adminService.GetById(id));
        }
        [HttpPost]
        public ActionResult<Admin> Create([FromBody] CreationAdminDto creationAdminDto)
        {
            return Ok(_adminService.Create(creationAdminDto));
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] CreationAdminDto creationAdminDto)
        {
            _adminService.Update(id, creationAdminDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _adminService.Delete(id);
            return NoContent();
        }
    }
}
