namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchasePrice
    {
        float Value { get; }
        string Label { get; }
        string IsoCode { get; }
    }
}
