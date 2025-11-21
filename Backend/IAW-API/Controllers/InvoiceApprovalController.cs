using IAW_API.Applications.InvoiceApprovals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IAW_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceApprovalController : Controller
    {
        private ISender _sender;
        public InvoiceApprovalController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> InvoiceApproval([FromBody] InvoiceApprovalCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
