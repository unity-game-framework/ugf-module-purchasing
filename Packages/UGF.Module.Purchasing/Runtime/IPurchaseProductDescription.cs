using UGF.Description.Runtime;

namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseProductDescription : IDescription
    {
        string Id { get; }
        PurchaseProductType Type { get; }
    }
}
