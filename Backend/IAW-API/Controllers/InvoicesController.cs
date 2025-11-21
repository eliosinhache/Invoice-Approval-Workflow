using IAW_API.Applications.InvoiceApprovals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IAW_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : Controller
    {
        private ISender _sender;
        public InvoicesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("determine-approvers")]
        public async Task<IActionResult> InvoiceApprovals([FromBody] InvoiceApprovalCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
