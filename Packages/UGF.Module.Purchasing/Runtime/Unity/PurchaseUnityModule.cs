using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Initialize.Runtime;
using UGF.Logs.Runtime;
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

            var module = StandardPurchasingModule.Instance();
            var builder = ConfigurationBuilder.Instance(module);

            foreach ((_, PurchaseProductDescription description) in Description.Products)
            {
                ProductType productType = PurchaseUnityUtility.GetProductType(description.ProductType);

                builder.AddProduct(description.StoreId, productType);
            }

            m_store = new PurchaseUnityStore(builder);
            m_store.TransactionFailed += OnStoreTransactionFailed;

            Log.Debug("Purchase Unity module initialized", new
            {
                products = Description.Products.Count,
                store = module.appStore
            });
        }

        protected override async Task OnInitializeAsync()
        {
            await m_store.InitializeAsync();

            Log.Debug("Purchase Unity module store initialize complete", new
            {
                isInitialized = m_store.IsInitialized
            });
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            m_store.TransactionFailed -= OnStoreTransactionFailed;
            m_store = null;
        }

        protected override bool OnCheckAvailable()
        {
            if (m_store == null) throw new InitializeStateException();

            return m_store.IsInitialized;
        }

        protected override Task<IPurchaseTransaction> OnPurchaseAsync(string productId)
        {
        }

        protected override Task OnConfirmAsync(string transactionId)
        {
        }

        protected override Task<IList<string>> OnGetPendingTransactionAsync()
        {
        }

        protected override Task<IDictionary<string, IPurchaseProduct>> OnGetProductsAsync()
        {
            var result = new Dictionary<string, IPurchaseProduct>();
            ProductCollection products = m_store.Controller.products;

            foreach (Product unityProduct in products.set)
            {
                PurchaseProduct product = PurchaseUnityUtility.GetProduct(unityProduct);

                result.Add(unityProduct.definition.id, product);
            }

            return Task.FromResult<IDictionary<string, IPurchaseProduct>>(result);
        }

        private void OnStoreTransactionFailed(IPurchaseTransaction transaction)
        {
        }
    }
}
