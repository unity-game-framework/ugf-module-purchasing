using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.Initialize.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UGF.RuntimeTools.Runtime.Tasks;

namespace UGF.Module.Purchasing.Runtime
{
    public abstract class PurchaseModule<TDescription> : ApplicationModule<TDescription>, IPurchaseModule, IApplicationModuleAsync where TDescription : class, IPurchaseModuleDescription
    {
        public IProvider<string, IPurchaseProductDescription> Products { get; }
        public bool IsAvailable { get { return OnCheckAvailable(); } }
        public bool IsProcessingPurchase { get { return OnCheckProcessingPurchase(); } }

        private readonly Dictionary<string, string> m_productIds = new Dictionary<string, string>();
        private InitializeState m_state;

        protected PurchaseModule(TDescription description, IApplication application) : this(description, application, new Provider<string, IPurchaseProductDescription>())
        {
        }

        protected PurchaseModule(TDescription description, IApplication application, IProvider<string, IPurchaseProductDescription> products) : base(description, application)
        {
            Products = products ?? throw new ArgumentNullException(nameof(products));
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Products.Added += OnProductAdded;
            Products.Removed += OnProductRemoved;

            foreach ((string key, IBuilder<IPurchaseProductDescription> value) in Description.Products)
            {
                IPurchaseProductDescription description = value.Build();

                Products.Add(key, description);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Products.Added -= OnProductAdded;
            Products.Removed -= OnProductRemoved;

            m_state = m_state.Uninitialize();
            m_productIds.Clear();

            Products.Clear();
        }

        public Task InitializeAsync()
        {
            m_state = m_state.Initialize();

            return OnInitializeAsync();
        }

        public Task<bool> PurchaseStartAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");
            if (IsProcessingPurchase) throw new InvalidOperationException("Purchasing processing purchase already.");

            return OnPurchaseStartAsync(id);
        }

        public Task PurchaseConfirmAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");
            if (IsProcessingPurchase) throw new InvalidOperationException("Purchasing processing purchase already.");

            return OnPurchaseConfirmAsync(id);
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

        public Task<TaskResult<string>> TryGetTransactionIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");

            return OnTryGetTransactionIdAsync(id);
        }

        public string GetProductDescriptionId(string productId)
        {
            return TryGetProductDescriptionId(productId, out string id) ? id : throw new ArgumentException($"Product description id not found by the specified product id: '{productId}'.");
        }

        public bool TryGetProductDescriptionId(string productId, out string id)
        {
            if (string.IsNullOrEmpty(productId)) throw new ArgumentException("Value cannot be null or empty.", nameof(productId));

            return m_productIds.TryGetValue(productId, out id);
        }

        protected virtual Task OnInitializeAsync()
        {
            return Task.CompletedTask;
        }

        protected abstract bool OnCheckAvailable();
        protected abstract bool OnCheckProcessingPurchase();
        protected abstract Task<bool> OnPurchaseStartAsync(string id);
        protected abstract Task OnPurchaseConfirmAsync(string id);
        protected abstract Task<IList<string>> OnGetPendingProductsAsync();
        protected abstract Task<IDictionary<string, IPurchaseProduct>> OnGetProductsAsync();
        protected abstract Task<TaskResult<string>> OnTryGetTransactionIdAsync(string id);

        protected virtual void OnProductAdded(string id, IPurchaseProductDescription description)
        {
        }

        protected virtual void OnProductRemoved(string id, IPurchaseProductDescription description)
        {
        }

        private void OnProductAdded(IProvider provider, string id, IPurchaseProductDescription entry)
        {
            m_productIds.Add(entry.Id, id);

            OnProductAdded(id, entry);
        }

        private void OnProductRemoved(IProvider provider, string id, IPurchaseProductDescription entry)
        {
            m_productIds.Remove(entry.Id);

            OnProductRemoved(id, entry);
        }
    }
}
