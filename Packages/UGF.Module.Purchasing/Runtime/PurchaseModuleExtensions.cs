using System;
using System.Threading.Tasks;

namespace UGF.Module.Purchasing.Runtime
{
    public static class PurchaseModuleExtensions
    {
        public static async Task<bool> PurchaseAsync(this IPurchaseModule purchaseModule, string id)
        {
            if (purchaseModule == null) throw new ArgumentNullException(nameof(purchaseModule));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            if (await purchaseModule.PurchaseStartAsync(id))
            {
                await purchaseModule.PurchaseConfirmAsync(id);
                return true;
            }

            return false;
        }
    }
}
