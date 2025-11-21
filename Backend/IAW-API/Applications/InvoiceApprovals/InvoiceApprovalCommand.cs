using IAW_API.Models.Responses;
using IAW_API.Models.Responses.Common;
using MediatR;

namespace IAW_API.Applications.InvoiceApprovals
{
    public class InvoiceApprovalCommand : IRequest<Result<InvoiceApprovalResponse>>
    {
        public decimal Amount { get; set; }
        public bool IsPreferredVendor { get; set; }
    }
}
