using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    public class PurchaseUnityModule : PurchaseModule<PurchaseUnityModuleDescription>
    {
        public PurchaseUnityModule(PurchaseUnityModuleDescription description, IApplication application) : base(description, application)
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
        }

        protected override Task OnInitializeAsync()
        {
            return base.OnInitializeAsync();
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();
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
