using System;
using UGF.Description.Runtime;

namespace UGF.Module.Purchasing.Runtime
{
    public class PurchaseProductDescription : DescriptionBase, IPurchaseProductDescription
    {
        public PurchaseProductId Id { get; }
        public PurchaseProductType Type { get; }

        public PurchaseProductDescription(PurchaseProductId id, PurchaseProductType type)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));

            Id = id;
            Type = type;
        }
    }
}
