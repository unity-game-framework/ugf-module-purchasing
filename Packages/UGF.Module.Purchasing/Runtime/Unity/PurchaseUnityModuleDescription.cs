using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    public class PurchaseUnityModuleDescription : ApplicationModuleDescription
    {
        public Dictionary<string, PurchaseProductDescription> Products { get; } = new Dictionary<string, PurchaseProductDescription>();
    }
}
