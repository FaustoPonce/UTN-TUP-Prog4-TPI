using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutPlanService;
        public WorkoutPlanController(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }
        [HttpGet]
        public ActionResult GetAllWorkoutPlans()
        {
            return Ok(_workoutPlanService.GetAllWorkoutPlans());
        }
        
        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            return Ok(_workoutPlanService.GetById(id));
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Recepcionista")]
        public ActionResult Create([FromBody] CreationWorkoutPlanDto creationWorkoutPlanDto)
        {
            return Ok(_workoutPlanService.Create(creationWorkoutPlanDto));
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Recepcionista")]
        public ActionResult Update([FromRoute] int id, [FromBody] CreationWorkoutPlanDto creationWorkoutPlanDto)
        {
            _workoutPlanService.Update(id, creationWorkoutPlanDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Recepcionista")]
        public ActionResult Delete([FromRoute] int id)
        {
            _workoutPlanService.Delete(id);
            return NoContent();
        }
    }
}
