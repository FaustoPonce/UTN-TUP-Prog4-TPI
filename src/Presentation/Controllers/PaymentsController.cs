using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;

        }
        [HttpGet]
        public ActionResult GetAllPayments()
        {
            return Ok(_paymentService.GetAllPayments());
        }
        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            return Ok(_paymentService.GetById(id));
        }
        [HttpPost]
        public ActionResult Create([FromBody] CreationPaymentDto creationPaymentDto)
        {
            return Ok(_paymentService.Create(creationPaymentDto));
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] CreationPaymentDto creationPaymentDto)
        {
            _paymentService.Update(id, creationPaymentDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _paymentService.Delete(id);
            return NoContent();
        }
    }
}
