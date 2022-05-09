namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseTransaction
    {
        string Id { get; }
        string Receipt { get; }
        bool HasReceipt { get; }
    }
}
