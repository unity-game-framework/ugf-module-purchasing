using System;
using System.Threading.Tasks;

namespace UGF.Module.Purchasing.Runtime
{
    public static class PurchaseModuleExtensions
    {
        public static async Task<bool> PurchaseAsync(this IPurchaseModule purchaseModule, string productId)
        {
            if (purchaseModule == null) throw new ArgumentNullException(nameof(purchaseModule));
            if (string.IsNullOrEmpty(productId)) throw new ArgumentException("Value cannot be null or empty.", nameof(productId));

            if (await purchaseModule.PurchaseStartAsync(productId))
            {
                await purchaseModule.PurchaseConfirmAsync(productId);
                return true;
            }

            return false;
        }
    }
}
