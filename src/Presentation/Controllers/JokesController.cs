using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Application.Models;
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
        public async Task<ActionResult<JokeDto>> GetRandomJoke()
        {
            var dto = await _jokeService.GetRandomJokeAsync();
            return Ok(dto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<JokeDto>> GetJokeById([FromRoute] int id)
        {
            var dto = await _jokeService.GetJokeByIdAsync(id);
            return Ok(dto);
        }
        [HttpGet("Get-random-by-type")]
        public async Task<ActionResult<List<JokeDto>>> GetRandomJokeByType([FromQuery] string type)
        {
            var dto = await _jokeService.GetRandomJokeByTypeAsync(type);
            return Ok(dto);
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<string>>> GetJokeTypeAsync()
        {
            var joketypes = await _jokeService.GetJokeTypes();
            return Ok(joketypes);
        }
    }
}
