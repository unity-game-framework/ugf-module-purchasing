using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Tasks;
using UnityEngine.Purchasing;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    public class PurchaseUnityModule : PurchaseModule<PurchaseUnityModuleDescription>
    {
        public PurchaseUnityStore Store { get { return m_store ?? throw new ArgumentException("Value not specified."); } }

        private readonly HashSet<string> m_pending = new HashSet<string>();
        private PurchaseUnityStore m_store;
        private bool m_processingPurchase;
        private bool? m_processingPurchaseResult;

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
            m_store.PurchasePending += OnStorePurchasePending;
            m_store.PurchaseFailed += OnStorePurchaseFailed;

            Log.Debug("Purchase Unity module initialized", new
            {
                products = Description.Products.Count,
                store = module.appStore
            });
        }

        protected override async Task OnInitializeAsync()
        {
            await Store.InitializeAsync();

            Log.Debug("Purchase Unity module store initialize complete", new
            {
                isInitialized = m_store.IsInitialized
            });
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            m_store.PurchasePending -= OnStorePurchasePending;
            m_store.PurchaseFailed -= OnStorePurchaseFailed;
            m_store = null;
        }

        protected override bool OnCheckAvailable()
        {
            return Store.IsInitialized;
        }

        protected override bool OnCheckProcessingPurchase()
        {
            return m_processingPurchase;
        }

        protected override async Task<bool> OnPurchaseStartAsync(string productId)
        {
            Product product = Store.Controller.products.WithID(productId);

            if (product == null) throw new ArgumentException($"Product not found by the specified id: '{productId}'.");

            m_processingPurchase = true;

            try
            {
                Store.Controller.InitiatePurchase(product);

                while (m_processingPurchaseResult == null)
                {
                    await Task.Yield();
                }

                bool result = m_processingPurchaseResult.Value;

                return result;
            }
            finally
            {
                m_processingPurchase = false;
                m_processingPurchaseResult = null;
            }
        }

        protected override Task OnPurchaseConfirmAsync(string productId)
        {
            if (!m_pending.Contains(productId)) throw new ArgumentException($"Pending product not found by the specified id: '{productId}'.");

            Product product = Store.Controller.products.WithID(productId);

            if (product == null) throw new ArgumentException($"Product not found by the specified id: '{productId}'.");

            Store.Controller.ConfirmPendingPurchase(product);

            m_pending.Remove(productId);

            return Task.CompletedTask;
        }

        protected override Task<IList<string>> OnGetPendingProductsAsync()
        {
            var result = new List<string>();

            foreach (string productId in m_pending)
            {
                result.Add(productId);
            }

            return Task.FromResult<IList<string>>(result);
        }

        protected override Task<IDictionary<string, IPurchaseProduct>> OnGetProductsAsync()
        {
            var result = new Dictionary<string, IPurchaseProduct>();
            ProductCollection products = Store.Controller.products;

            foreach (Product unityProduct in products.set)
            {
                PurchaseProduct product = PurchaseUnityUtility.GetProduct(unityProduct);

                result.Add(unityProduct.definition.id, product);
            }

            return Task.FromResult<IDictionary<string, IPurchaseProduct>>(result);
        }

        protected override Task<TaskResult<string>> OnTryGetTransactionIdAsync(string productId)
        {
            Product product = Store.Controller.products.WithID(productId);

            return product != null && !string.IsNullOrEmpty(product.transactionID)
                ? Task.FromResult<TaskResult<string>>(product.transactionID)
                : Task.FromResult(TaskResult<string>.Empty);
        }

        private void OnStorePurchasePending(string productId)
        {
            m_pending.Add(productId);

            if (m_processingPurchase)
            {
                m_processingPurchaseResult = true;
            }
        }

        private void OnStorePurchaseFailed(string productId)
        {
            m_processingPurchaseResult = false;
        }
    }
}
