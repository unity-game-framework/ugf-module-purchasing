﻿#if UGF_MODULE_PURCHASING_PURCHASING_INSTALLED
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Tasks;
using UnityEngine.Purchasing;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    public class PurchaseUnityModule : PurchaseModule<PurchaseUnityModuleDescription>
    {
        public PurchaseUnityStore Store { get { return m_store ?? throw new ArgumentException("Value not specified."); } }

        private readonly HashSet<PurchaseProductId> m_pending = new HashSet<PurchaseProductId>();
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

            foreach ((_, IPurchaseProductDescription value) in Products.Entries)
            {
                ProductType productType = PurchaseUnityUtility.GetProductType(value.Type);

                builder.AddProduct(value.Id.Value, productType);
            }

            m_store = new PurchaseUnityStore(builder);
            m_store.PurchasePending += OnStorePurchasePending;
            m_store.PurchaseFailed += OnStorePurchaseFailed;

            Logger.Debug("Purchase Unity module initialized", new
            {
                products = Description.Products.Count,
                store = module.appStore
            });
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();
            await Store.InitializeAsync();

            Logger.Debug("Purchase Unity module store initialize complete", new
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

        protected override async Task<bool> OnPurchaseStartAsync(GlobalId id)
        {
            IPurchaseProductDescription description = Products.Get(id);

            Product product = Store.GetProduct(description.Id.Value);

            Logger.Debug("Purchase Unity module start purchase", new
            {
                id,
                productId = product.definition.id
            });

            m_processingPurchase = true;

            try
            {
                Store.Controller.InitiatePurchase(product);

                while (m_processingPurchaseResult == null)
                {
                    await Task.Yield();
                }

                return m_processingPurchaseResult.Value;
            }
            finally
            {
                m_processingPurchase = false;
                m_processingPurchaseResult = null;
            }
        }

        protected override Task OnPurchaseConfirmAsync(GlobalId id)
        {
            IPurchaseProductDescription description = Products.Get(id);

            if (!m_pending.Contains(description.Id)) throw new ArgumentException($"Pending product not found by the specified id: '{description.Id}'.");

            Product product = Store.GetProduct(description.Id.Value);

            Logger.Debug("Purchase Unity module confirming purchase", new
            {
                id,
                productId = product.definition.id
            });

            Store.Controller.ConfirmPendingPurchase(product);

            m_pending.Remove(description.Id);

            return Task.CompletedTask;
        }

        protected override Task<IList<GlobalId>> OnGetPendingProductsAsync()
        {
            var result = new List<GlobalId>();

            foreach (PurchaseProductId productId in m_pending)
            {
                if (TryGetProductDescriptionId(productId, out GlobalId id))
                {
                    result.Add(id);
                }
#if UGF_LOG_DEBUG
                else
                {
                    Logger.Debug("Pending product description not found by the specified product id", new
                    {
                        productId
                    });
                }
#endif
            }

            return Task.FromResult<IList<GlobalId>>(result);
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

        protected override Task<TaskResult<string>> OnTryGetTransactionIdAsync(GlobalId id)
        {
            IPurchaseProductDescription description = Products.Get(id);

            Product product = Store.GetProduct(description.Id.Value);

            return product != null && !string.IsNullOrEmpty(product.transactionID)
                ? Task.FromResult<TaskResult<string>>(product.transactionID)
                : Task.FromResult(TaskResult<string>.Empty);
        }

        private void OnStorePurchasePending(string productId)
        {
            m_pending.Add(new PurchaseProductId(productId));

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
#endif
