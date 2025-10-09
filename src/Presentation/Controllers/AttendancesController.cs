using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        [HttpGet]
        public ActionResult GetAllAttendaces()
        {
            return Ok(_attendanceService.GetAllAttendaces());
        }
        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            return Ok(_attendanceService.GetById(id));
        }
        [HttpPost]
        public ActionResult Create([FromBody] CreationAttendanceDto creationAttendaceDto)
        {
            return Ok(_attendanceService.Create(creationAttendaceDto));
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] CreationAttendanceDto creationAttendaceDto)
        {
            _attendanceService.Update(id, creationAttendaceDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _attendanceService.Delete(id);
            return NoContent();
        }
    }
}
