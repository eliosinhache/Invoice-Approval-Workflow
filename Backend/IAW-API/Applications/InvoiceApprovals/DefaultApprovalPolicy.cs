using IAW_API.Models;

namespace IAW_API.Applications.InvoiceApprovals
{
    public class DefaultApprovalPolicy : IApprovalPolicy
    {
        private const decimal Tier1Upper = 999m;
        private const decimal Tier2Upper = 9999m;

        public IReadOnlyList<string> GetRequiredApprovers(decimal amount, bool isPreferredVendor)
        {
            if (amount < 0) return Array.Empty<string>();

            int approverCount = amount switch
            {
                < Tier1Upper + 1m => 1,
                <= Tier2Upper => 2,
                _ => 3
            };

            var approvers = Roles.AvailableRoles.Take(approverCount).ToList();

            if (isPreferredVendor && approvers.Count > 0)
            {
                approvers.RemoveAt(0);
            }

            return approvers;
        }
    }
}