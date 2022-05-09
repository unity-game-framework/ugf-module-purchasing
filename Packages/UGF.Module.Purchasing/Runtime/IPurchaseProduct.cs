namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseProduct
    {
        bool Available { get; }
        float Price { get; }
        string PriceLabel { get; }
        string PriceIsoCode { get; }
        string Receipt { get; }
        bool HasReceipt { get; }
    }
}
