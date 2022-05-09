using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    public class PurchaseModuleUnity : PurchaseModule<PurchaseModuleUnityDescription>
    {
        public PurchaseModuleUnity(PurchaseModuleUnityDescription description, IApplication application) : base(description, application)
        {
        }

        protected override Task<IPurchaseTransaction> OnPurchaseAsync(string productId)
        {
            throw new System.NotImplementedException();
        }

        protected override Task OnConfirmAsync(string transactionId)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<IList<string>> OnGetPendingTransactionAsync()
        {
            throw new System.NotImplementedException();
        }

        protected override Task<IDictionary<string, IPurchaseProduct>> OnGetProductsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
