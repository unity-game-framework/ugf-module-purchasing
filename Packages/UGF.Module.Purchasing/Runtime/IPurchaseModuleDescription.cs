using System.Collections.Generic;
using UGF.Description.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseModuleDescription : IDescription
    {
        public IReadOnlyDictionary<GlobalId, IPurchaseProductDescription> Products { get; }
    }
}
