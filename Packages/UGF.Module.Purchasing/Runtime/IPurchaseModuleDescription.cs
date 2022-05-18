using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;

namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseModuleDescription : IApplicationModuleDescription
    {
        public IReadOnlyDictionary<string, IBuilder<IPurchaseProductDescription>> Products { get; }
    }
}
