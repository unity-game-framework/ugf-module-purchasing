using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    public class PurchaseUnityModuleDescription : PurchaseModuleDescription
    {
        public PurchaseUnityModuleDescription(IReadOnlyDictionary<GlobalId, IPurchaseProductDescription> products) : base(products)
        {
        }
    }
}
