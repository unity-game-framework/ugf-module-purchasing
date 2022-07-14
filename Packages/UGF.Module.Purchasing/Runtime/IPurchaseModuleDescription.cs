using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseModuleDescription : IApplicationModuleDescription
    {
        public IReadOnlyDictionary<GlobalId, IBuilder<IPurchaseProductDescription>> Products { get; }
    }
}
