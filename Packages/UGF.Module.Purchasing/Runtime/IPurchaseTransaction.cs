namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseTransaction
    {
        string Id { get; }
        string ProductId { get; }
        string Receipt { get; }
        bool HasReceipt { get; }
    }
}
