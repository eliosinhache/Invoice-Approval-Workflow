using IAW_API.Commons;
using IAW_API.Models;
using IAW_API.Models.Responses;
using MediatR;

namespace IAW_API.Applications.InvoiceApprovals
{
    public class InvoiceApprovalsHandler : IRequestHandler<InvoiceApprovalCommand, Result<InvoiceApprovalResponse>>
    {
        public async Task<Result<InvoiceApprovalResponse>> Handle(InvoiceApprovalCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<InvoiceApprovalResponse>();

            var requiredApprovers = ApprovalsListByAmount(request.Amount);

            if (request.IsPreferredVendor)
            {
                requiredApprovers.RemoveAt(0);
            }

            result.Success(new InvoiceApprovalResponse
            {
                RequiredApprovers = requiredApprovers
            });

            return result;
        }

        private List<string> ApprovalsListByAmount(decimal amount)
        {
            int amountOfAppovers = 0;
            switch (amount)
            {
                case < 1000:
                    amountOfAppovers = 1;
                    break;
                case <= 9999:
                    amountOfAppovers = 2;
                    break;
                default:
                    amountOfAppovers = 3;
                    break;
            }
            return Roles.AvailableRoles.Take(amountOfAppovers).ToList();
        }
    }
}
