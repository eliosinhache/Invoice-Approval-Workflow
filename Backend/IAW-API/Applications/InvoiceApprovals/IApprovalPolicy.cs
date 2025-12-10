namespace IAW_API.Applications.InvoiceApprovals
{
    public interface IApprovalPolicy
    {
        IReadOnlyList<string> GetRequiredApprovers(decimal amount, bool isPreferredVendor);
    }
}