using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Tasks;

namespace UGF.Module.Purchasing.Runtime
{
    public interface IPurchaseStore
    {
        Task<TaskResult<IPurchaseTransaction>> PurchaseAsync(string productId);
        Task ConfirmAsync(string transactionId);
        Task<IDictionary<string, IPurchaseProduct>> GetProductsAsync();
    }
}
