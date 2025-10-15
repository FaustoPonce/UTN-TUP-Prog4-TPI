using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticacionService _authenticacionService;
        public AuthController(IAuthenticacionService authenticacionService)
        {
            _authenticacionService = authenticacionService;
        }
        [HttpPost]
        public ActionResult<string> Authenticate([FromBody] LoginDto loginDto)
        {
            string newToken = _authenticacionService.Authenticate(loginDto);

            return newToken;
        }
    }
}
