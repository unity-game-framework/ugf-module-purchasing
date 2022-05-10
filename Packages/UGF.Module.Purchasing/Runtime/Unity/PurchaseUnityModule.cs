using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UnityEngine.Purchasing;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    public class PurchaseUnityModule : PurchaseModule<PurchaseUnityModuleDescription>
    {
        private PurchaseUnityStore m_store;

        public PurchaseUnityModule(PurchaseUnityModuleDescription description, IApplication application) : base(description, application)
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach ((_, PurchaseProductDescription description) in Description.Products)
            {
                ProductType productType = PurchaseUnityUtility.GetProductType(description.ProductType);

                builder.AddProduct(description.StoreId, productType);
            }

            m_store = new PurchaseUnityStore(builder);
            m_store.TransactionFailed += OnStoreTransactionFailed;
        }

        protected override async Task OnInitializeAsync()
        {
            await m_store.InitializeAsync();
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            m_store.TransactionFailed -= OnStoreTransactionFailed;
            m_store = null;
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

        private void OnStoreTransactionFailed(IPurchaseTransaction transaction)
        {
        }
    }
}
