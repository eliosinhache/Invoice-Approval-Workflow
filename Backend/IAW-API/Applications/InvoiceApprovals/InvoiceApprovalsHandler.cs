using IAW_API.Models.Responses;
using IAW_API.Models.Responses.Common;
using MediatR;

namespace IAW_API.Applications.InvoiceApprovals
{
    public class InvoiceApprovalsHandler : IRequestHandler<InvoiceApprovalCommand, Result<InvoiceApprovalResponse>>
    {
        public async Task<Result<InvoiceApprovalResponse>> Handle(InvoiceApprovalCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<InvoiceApprovalResponse>();
            // Placeholder logic for determining required approvers
            var requiredApprovers = new List<string>
            {
                "GeneralManager",
            };

            result.Success(new InvoiceApprovalResponse
            {
                RequiredApprovers = requiredApprovers
            });

            return result;
        }
    }
}
