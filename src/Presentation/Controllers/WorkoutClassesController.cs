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
    public class WorkoutClassesController : ControllerBase
    {
        private readonly IWorkoutClassService _workoutClassService;

        public WorkoutClassesController(IWorkoutClassService workoutClassService)
        {
            _workoutClassService = workoutClassService;
        }

        [HttpGet]
        public ActionResult<List<WorkoutClassDto>> GetAllWorkoutClasses()
        {
            var workoutClasses = _workoutClassService.GetAllWorkoutClass();
            return Ok(workoutClasses);
        }
        [HttpGet("{id}")]
        public ActionResult<WorkoutClassDto> GetWorkoutClassById(int id)
        {
            var workoutClass = _workoutClassService.GetById(id);
            if (workoutClass == null)
            {
                return NotFound();
            }
            return Ok(workoutClass);
        }
        [HttpPost]
        [Authorize]
        public ActionResult<WorkoutClass> CreateWorkoutClass([FromBody] CreationWorkoutClassDto creationWorkoutClassDto)
        {
            var createdWorkoutClass = _workoutClassService.Create(creationWorkoutClassDto);
            return Ok(createdWorkoutClass);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult DeleteWorkoutClass(int id)
        {
            _workoutClassService.Delete(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult UpdateWorkoutClass(int id, [FromBody] CreationWorkoutClassDto creationWorkoutClassDto)
        {
            _workoutClassService.Update(id, creationWorkoutClassDto);
            return Ok();
        }
    }
}
