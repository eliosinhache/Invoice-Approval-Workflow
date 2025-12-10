using IAW_API.Commons;
using IAW_API.Models;
using IAW_API.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IAW_API.Applications.InvoiceApprovals
{
    public class InvoiceApprovalsHandler : IRequestHandler<InvoiceApprovalCommand, Result<InvoiceApprovalResponse>>
    {
        private readonly IApprovalPolicy _approvalPolicy;
        private readonly ILogger<InvoiceApprovalsHandler> _logger;

        public InvoiceApprovalsHandler(IApprovalPolicy approvalPolicy, ILogger<InvoiceApprovalsHandler> logger)
        {
            _approvalPolicy = approvalPolicy;
            _logger = logger;
        }

        public Task<Result<InvoiceApprovalResponse>> Handle(InvoiceApprovalCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = new Result<InvoiceApprovalResponse>();

            if (request.Amount < 0)
            {
                _logger.LogWarning("Invoice amount is negative: {Amount}", request.Amount);
                result.Success(new InvoiceApprovalResponse { RequiredApprovers = new List<string>() });
                return Task.FromResult(result);
            }

            var requiredApprovers = _approvalPolicy.GetRequiredApprovers(request.Amount, request.IsPreferredVendor).ToList();

            result.Success(new InvoiceApprovalResponse
            {
                RequiredApprovers = requiredApprovers
            });

            _logger.LogInformation("Determined {Count} approvers for amount {Amount} (preferredVendor: {Preferred})",
                requiredApprovers.Count, request.Amount, request.IsPreferredVendor);

            return Task.FromResult(result);
        }
    }
}
