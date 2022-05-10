using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Initialize.Runtime;

namespace UGF.Module.Purchasing.Runtime
{
    public abstract class PurchaseModule<TDescription> : ApplicationModule<TDescription>, IPurchaseModule, IApplicationModuleAsync where TDescription : class, IApplicationModuleDescription
    {
        public bool IsAvailable { get { return OnCheckAvailable(); } }

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

        public Task<IPurchaseTransaction> PurchaseAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId)) throw new ArgumentException("Value cannot be null or empty.", nameof(productId));
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");

            return OnPurchaseAsync(productId);
        }

        public Task ConfirmAsync(string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) throw new ArgumentException("Value cannot be null or empty.", nameof(transactionId));
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");

            return OnConfirmAsync(transactionId);
        }

        public Task<IList<string>> GetPendingTransactionsAsync()
        {
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");

            return OnGetPendingTransactionAsync();
        }

        public Task<IDictionary<string, IPurchaseProduct>> GetProductsAsync()
        {
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");

            return OnGetProductsAsync();
        }

        protected virtual Task OnInitializeAsync()
        {
            return Task.CompletedTask;
        }

        protected abstract bool OnCheckAvailable();
        protected abstract Task<IPurchaseTransaction> OnPurchaseAsync(string productId);
        protected abstract Task OnConfirmAsync(string transactionId);
        protected abstract Task<IList<string>> OnGetPendingTransactionAsync();
        protected abstract Task<IDictionary<string, IPurchaseProduct>> OnGetProductsAsync();
    }
}
