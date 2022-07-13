using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Purchasing.Runtime
{
    public class PurchaseModuleDescription : ApplicationModuleDescription, IPurchaseModuleDescription
    {
        public Dictionary<GlobalId, IBuilder<IPurchaseProductDescription>> Products { get; } = new Dictionary<GlobalId, IBuilder<IPurchaseProductDescription>>();

        IReadOnlyDictionary<GlobalId, IBuilder<IPurchaseProductDescription>> IPurchaseModuleDescription.Products { get { return Products; } }
    }
}
