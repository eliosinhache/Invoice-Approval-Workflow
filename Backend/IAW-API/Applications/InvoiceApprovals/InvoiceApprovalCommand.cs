using IAW_API.Commons;
using IAW_API.Models.Responses;
using MediatR;

namespace IAW_API.Applications.InvoiceApprovals
{
    public class InvoiceApprovalCommand : IRequest<Result<InvoiceApprovalResponse>>
    {
        public decimal Amount { get; set; }
        public bool IsPreferredVendor { get; set; }
    }
}
