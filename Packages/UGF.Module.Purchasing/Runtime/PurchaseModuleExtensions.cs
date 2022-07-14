using System;
using System.Threading.Tasks;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Purchasing.Runtime
{
    public static class PurchaseModuleExtensions
    {
        public static async Task<bool> PurchaseAsync(this IPurchaseModule purchaseModule, GlobalId id)
        {
            if (purchaseModule == null) throw new ArgumentNullException(nameof(purchaseModule));
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));

            if (await purchaseModule.PurchaseStartAsync(id))
            {
                await purchaseModule.PurchaseConfirmAsync(id);
                return true;
            }

            return false;
        }
    }
}
