using UGF.Description.Runtime;

namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseProductDescription : IDescription
    {
        PurchaseProductId Id { get; }
        PurchaseProductType Type { get; }
    }
}
