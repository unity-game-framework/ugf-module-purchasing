using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UGF.RuntimeTools.Runtime.Tasks;

namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseModule : IApplicationModule
    {
        new IPurchaseModuleDescription Description { get; }
        IProvider<string, IPurchaseProductDescription> Products { get; }
        bool IsAvailable { get; }
        bool IsProcessingPurchase { get; }

        Task<bool> PurchaseStartAsync(string id);
        Task PurchaseConfirmAsync(string id);
        Task<IList<string>> GetPendingProductsAsync();
        Task<IDictionary<string, IPurchaseProduct>> GetProductsAsync();
        Task<TaskResult<string>> TryGetTransactionIdAsync(string id);
        string GetProductDescriptionId(string productId);
        bool TryGetProductDescriptionId(string productId, out string id);
    }
}
