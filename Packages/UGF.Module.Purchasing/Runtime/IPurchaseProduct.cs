namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseProduct
    {
        bool Available { get; }
        IPurchasePrice Price { get; }
        string Receipt { get; }
        bool HasReceipt { get; }
    }
}
