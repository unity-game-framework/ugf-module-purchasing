using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Providers;
using UGF.RuntimeTools.Runtime.Tasks;

namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseModule : IApplicationModule
    {
        new IPurchaseModuleDescription Description { get; }
        IProvider<GlobalId, IPurchaseProductDescription> Products { get; }
        bool IsAvailable { get; }
        bool IsProcessingPurchase { get; }

        Task<bool> PurchaseStartAsync(GlobalId id);
        Task PurchaseConfirmAsync(GlobalId id);
        Task<IList<GlobalId>> GetPendingProductsAsync();
        Task<IDictionary<string, IPurchaseProduct>> GetProductsAsync();
        Task<TaskResult<string>> TryGetTransactionIdAsync(GlobalId id);
        GlobalId GetProductDescriptionId(string productId);
        bool TryGetProductDescriptionId(string productId, out GlobalId id);
    }
}
