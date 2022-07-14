using UGF.Description.Runtime;

namespace UGF.Module.Purchasing.Runtime
{
    public class PurchaseProductDescription : DescriptionBase, IPurchaseProductDescription
    {
        public PurchaseProductId Id { get; set; }
        public PurchaseProductType Type { get; set; }
    }
}
