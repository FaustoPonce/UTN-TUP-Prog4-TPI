using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetAllUsers() 
        {
            return Ok(_userService.GetAllUsers());
        }
        
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById([FromRoute] int id) 
        {
            return Ok(_userService.GetById(id));
        }
        [HttpPost]
        [Authorize]
        public ActionResult<User> Create([FromBody] CreationUserDto creationUserDto) 
        { 
            return Ok(_userService.Create(creationUserDto));
        }
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Update([FromRoute] int id, [FromBody] CreationUserDto creationUserDto) 
        {
            _userService.Update(id, creationUserDto);
            Console.WriteLine("User updated successfully.");
            return Ok(); 
        }
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete([FromRoute] int id) 
        {
            _userService.Delete(id);
            return NoContent();
        }
    }
}
