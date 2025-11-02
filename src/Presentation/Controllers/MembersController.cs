using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        [HttpGet]
        public ActionResult GetAllMembers()
        {
            return Ok(_memberService.GetAllMembers());
        }
        [HttpGet("{id}")]
        public ActionResult GetMember(int id)
        {
            return Ok(_memberService.GetById(id));

        }
        [HttpPost]
        [Authorize(Roles = "Admin,Recepcionista")]
        public ActionResult Create([FromBody] CreationMemberDto creationMemberDto)
        {
            return Ok(_memberService.Create(creationMemberDto));
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Recepcionista")]
        public ActionResult DeleteMember(int id)
        {
            _memberService.Delete(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Recepcionista")]
        public ActionResult UpdateMember(int id, [FromBody] CreationMemberDto creationMemberDto)
        {
            _memberService.Update(id, creationMemberDto);
            return Ok();
        }
    }
}
