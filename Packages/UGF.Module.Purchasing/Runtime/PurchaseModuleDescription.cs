using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;

namespace UGF.Module.Purchasing.Runtime
{
    public class PurchaseModuleDescription : ApplicationModuleDescription, IPurchaseModuleDescription
    {
        public Dictionary<string, IBuilder<IPurchaseProductDescription>> Products { get; } = new Dictionary<string, IBuilder<IPurchaseProductDescription>>();

        IReadOnlyDictionary<string, IBuilder<IPurchaseProductDescription>> IPurchaseModuleDescription.Products { get { return Products; } }
    }
}
