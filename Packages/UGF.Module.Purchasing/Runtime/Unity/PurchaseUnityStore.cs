using System;
using System.Threading.Tasks;
using UGF.Initialize.Runtime;
using UGF.Logs.Runtime;
using UnityEngine.Purchasing;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    public class PurchaseUnityStore : IStoreListener
    {
        public ConfigurationBuilder Builder { get; }
        public bool IsInitialized { get { return m_initializeResult ?? false; } }
        public IStoreController Controller { get { return m_controller ?? throw new ArgumentException("Value not specified."); } }
        public IExtensionProvider Extensions { get { return m_extensions ?? throw new ArgumentException("Value not specified."); } }

        public event PurchaseUnityProductHandler PurchasePending;
        public event PurchaseUnityProductHandler PurchaseFailed;

        private InitializeState m_state;
        private bool? m_initializeResult;
        private IStoreController m_controller;
        private IExtensionProvider m_extensions;

        public PurchaseUnityStore(ConfigurationBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public async Task<bool> InitializeAsync()
        {
            m_state = m_state.Initialize();

            Log.Debug("Unity store initializing.");

            UnityPurchasing.Initialize(this, Builder);

            while (m_initializeResult == null)
            {
                await Task.Yield();
            }

            return m_initializeResult.Value;
        }

        public Product GetProduct(string id)
        {
            return TryGetProduct(id, out Product product) ? product : throw new ArgumentException($"Product not found by the specified id: '{id}'.");
        }

        public bool TryGetProduct(string id, out Product product)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            product = Controller.products.WithID(id);

            return product != null;
        }

        void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            m_initializeResult = true;
            m_controller = controller;
            m_extensions = extensions;

            Log.Debug("Unity store initialized", new
            {
                controller
            });
        }

        void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
        {
            m_initializeResult = false;

            Log.Debug("Unity store initialization failed", new
            {
                error
            });
        }

        PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs arguments)
        {
            Product product = arguments.purchasedProduct;

            PurchasePending?.Invoke(product.definition.id);

            return PurchaseProcessingResult.Pending;
        }

        void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason reason)
        {
            PurchaseFailed?.Invoke(product.definition.id);

            Log.Debug("Unity store purchase failed", new
            {
                productId = product.definition.id,
                transactionId = product.transactionID,
                reason
            });
        }
    }
}
