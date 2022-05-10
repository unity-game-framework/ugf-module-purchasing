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

        public event PurchaseTransactionHandler TransactionFailed;

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

            Log.Debug("Unity store initializing");

            UnityPurchasing.Initialize(this, Builder);

            while (m_initializeResult == null)
            {
                await Task.Yield();
            }

            return m_initializeResult.Value;
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

            return PurchaseProcessingResult.Complete;
        }

        void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason reason)
        {
            var transaction = new PurchaseTransaction(product.transactionID, product.definition.id);

            if (product.hasReceipt)
            {
                transaction.SetReceipt(product.receipt);
            }

            TransactionFailed?.Invoke(transaction);

            Log.Debug("Unity store purchase failed", new
            {
                productId = product.definition.id,
                transactionId = product.transactionID,
                reason
            });
        }
    }
}
