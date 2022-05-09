using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;

namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseModule : IApplicationModule
    {
        Task<IPurchaseTransaction> PurchaseAsync(string productId);
        Task ConfirmAsync(string transactionId);
        Task<IList<string>> GetPendingTransactionsAsync();
        Task<IDictionary<string, IPurchaseProduct>> GetProductsAsync();
    }
}
