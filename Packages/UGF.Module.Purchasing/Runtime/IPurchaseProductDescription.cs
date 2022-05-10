using UGF.Description.Runtime;

namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseProductDescription : IDescription
    {
        string StoreId { get; }
        PurchaseProductType ProductType { get; }
    }
}
