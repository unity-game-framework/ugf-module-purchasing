using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Purchasing.Runtime
{
    public class PurchaseModuleDescription : ApplicationModuleDescription, IPurchaseModuleDescription
    {
        public IReadOnlyDictionary<GlobalId, IPurchaseProductDescription> Products { get; }

        public PurchaseModuleDescription(IReadOnlyDictionary<GlobalId, IPurchaseProductDescription> products)
        {
            Products = products ?? throw new ArgumentNullException(nameof(products));
        }
    }
}
