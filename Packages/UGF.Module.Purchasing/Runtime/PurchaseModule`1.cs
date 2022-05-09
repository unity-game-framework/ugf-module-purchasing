using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;

namespace UGF.Module.Purchasing.Runtime
{
    public abstract class PurchaseModule<TDescription> : ApplicationModule<TDescription>, IPurchaseModule, IApplicationModuleAsync where TDescription : class, IApplicationModuleDescription
    {
        protected PurchaseModule(TDescription description, IApplication application) : base(description, application)
        {
        }

        public Task InitializeAsync()
        {
            return OnInitializeAsync();
        }

        public Task<IPurchaseTransaction> PurchaseAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId)) throw new ArgumentException("Value cannot be null or empty.", nameof(productId));

            return OnPurchaseAsync(productId);
        }

        public Task ConfirmAsync(string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) throw new ArgumentException("Value cannot be null or empty.", nameof(transactionId));

            return OnConfirmAsync(transactionId);
        }

        public Task<IList<string>> GetPendingTransactionsAsync()
        {
            return OnGetPendingTransactionAsync();
        }

        public Task<IDictionary<string, IPurchaseProduct>> GetProductsAsync()
        {
            return OnGetProductsAsync();
        }

        protected virtual Task OnInitializeAsync()
        {
            return Task.CompletedTask;
        }

        protected abstract Task<IPurchaseTransaction> OnPurchaseAsync(string productId);
        protected abstract Task OnConfirmAsync(string transactionId);
        protected abstract Task<IList<string>> OnGetPendingTransactionAsync();
        protected abstract Task<IDictionary<string, IPurchaseProduct>> OnGetProductsAsync();
    }
}
