using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.RuntimeTools.Runtime.Tasks;

namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseModule : IApplicationModule
    {
        bool IsAvailable { get; }
        bool IsProcessingPurchase { get; }

        Task<bool> PurchaseStartAsync(string productId);
        Task PurchaseConfirmAsync(string productId);
        Task<IList<string>> GetPendingProductsAsync();
        Task<IDictionary<string, IPurchaseProduct>> GetProductsAsync();
        Task<TaskResult<string>> TryGetTransactionIdAsync(string productId);
    }
}
