using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Initialize.Runtime;
using UGF.RuntimeTools.Runtime.Tasks;

namespace UGF.Module.Purchasing.Runtime
{
    public abstract class PurchaseModule<TDescription> : ApplicationModule<TDescription>, IPurchaseModule, IApplicationModuleAsync where TDescription : class, IApplicationModuleDescription
    {
        public bool IsAvailable { get { return OnCheckAvailable(); } }
        public bool IsProcessingPurchase { get { return OnCheckProcessingPurchase(); } }

        private InitializeState m_state;

        protected PurchaseModule(TDescription description, IApplication application) : base(description, application)
        {
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            m_state = m_state.Uninitialize();
        }

        public Task InitializeAsync()
        {
            m_state = m_state.Initialize();

            return OnInitializeAsync();
        }

        public Task<bool> PurchaseStartAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId)) throw new ArgumentException("Value cannot be null or empty.", nameof(productId));
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");
            if (IsProcessingPurchase) throw new InvalidOperationException("Purchasing processing purchase already.");

            return OnPurchaseStartAsync(productId);
        }

        public Task PurchaseConfirmAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId)) throw new ArgumentException("Value cannot be null or empty.", nameof(productId));
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");
            if (IsProcessingPurchase) throw new InvalidOperationException("Purchasing processing purchase already.");

            return OnPurchaseConfirmAsync(productId);
        }

        public Task<IList<string>> GetPendingProductsAsync()
        {
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");

            return OnGetPendingProductsAsync();
        }

        public Task<IDictionary<string, IPurchaseProduct>> GetProductsAsync()
        {
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");

            return OnGetProductsAsync();
        }

        public Task<TaskResult<string>> TryGetTransactionIdAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId)) throw new ArgumentException("Value cannot be null or empty.", nameof(productId));
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");

            return OnTryGetTransactionIdAsync(productId);
        }

        protected virtual Task OnInitializeAsync()
        {
            return Task.CompletedTask;
        }

        protected abstract bool OnCheckAvailable();
        protected abstract bool OnCheckProcessingPurchase();
        protected abstract Task<bool> OnPurchaseStartAsync(string productId);
        protected abstract Task OnPurchaseConfirmAsync(string productId);
        protected abstract Task<IList<string>> OnGetPendingProductsAsync();
        protected abstract Task<IDictionary<string, IPurchaseProduct>> OnGetProductsAsync();
        protected abstract Task<TaskResult<string>> OnTryGetTransactionIdAsync(string productId);
    }
}
