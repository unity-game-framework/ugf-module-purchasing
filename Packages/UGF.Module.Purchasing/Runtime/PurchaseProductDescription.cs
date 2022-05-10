using UGF.Description.Runtime;

namespace UGF.Module.Purchasing.Runtime
{
    public class PurchaseProductDescription : DescriptionBase, IPurchaseProductDescription
    {
        public string StoreId { get; set; }
        public PurchaseProductType ProductType { get; set; }
    }
}
