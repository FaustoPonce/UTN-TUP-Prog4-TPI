using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly IjokeService _jokeService;

        public JokesController(IjokeService jokeservice)
        {
            _jokeService = jokeservice;
        }

        [HttpGet("random")]
        public async Task<ActionResult> GetRandomJoke()
        {
            var joke = await _jokeService.GetRandomJokeAsync();
            return Ok(joke);
        }
    }
}
