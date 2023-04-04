using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Providers;
using UGF.RuntimeTools.Runtime.Tasks;

namespace UGF.Module.Purchasing.Runtime
{
    public abstract class PurchaseModule<TDescription> : ApplicationModuleAsync<TDescription>, IPurchaseModule where TDescription : class, IPurchaseModuleDescription
    {
        public IProvider<GlobalId, IPurchaseProductDescription> Products { get; }
        public bool IsAvailable { get { return OnCheckAvailable(); } }
        public bool IsProcessingPurchase { get { return OnCheckProcessingPurchase(); } }

        IPurchaseModuleDescription IPurchaseModule.Description { get { return Description; } }

        private readonly Dictionary<PurchaseProductId, GlobalId> m_productIds = new Dictionary<PurchaseProductId, GlobalId>();

        protected PurchaseModule(TDescription description, IApplication application) : this(description, application, new Provider<GlobalId, IPurchaseProductDescription>())
        {
        }

        protected PurchaseModule(TDescription description, IApplication application, IProvider<GlobalId, IPurchaseProductDescription> products) : base(description, application)
        {
            Products = products ?? throw new ArgumentNullException(nameof(products));
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Products.Added += OnProductAdded;
            Products.Removed += OnProductRemoved;

            foreach ((GlobalId id, IPurchaseProductDescription description) in Description.Products)
            {
                Products.Add(id, description);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Products.Added -= OnProductAdded;
            Products.Removed -= OnProductRemoved;

            m_productIds.Clear();

            Products.Clear();
        }

        public Task<bool> PurchaseStartAsync(GlobalId id)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");
            if (IsProcessingPurchase) throw new InvalidOperationException("Purchasing processing purchase already.");

            return OnPurchaseStartAsync(id);
        }

        public Task PurchaseConfirmAsync(GlobalId id)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");
            if (IsProcessingPurchase) throw new InvalidOperationException("Purchasing processing purchase already.");

            return OnPurchaseConfirmAsync(id);
        }

        public Task<IList<GlobalId>> GetPendingProductsAsync()
        {
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");

            return OnGetPendingProductsAsync();
        }

        public Task<IDictionary<string, IPurchaseProduct>> GetProductsAsync()
        {
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");

            return OnGetProductsAsync();
        }

        public Task<TaskResult<string>> TryGetTransactionIdAsync(GlobalId id)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (!IsAvailable) throw new InvalidOperationException("Purchasing is unavailable.");

            return OnTryGetTransactionIdAsync(id);
        }

        public GlobalId GetProductDescriptionId(PurchaseProductId productId)
        {
            return TryGetProductDescriptionId(productId, out GlobalId id) ? id : throw new ArgumentException($"Product description id not found by the specified product id: '{productId}'.");
        }

        public bool TryGetProductDescriptionId(PurchaseProductId productId, out GlobalId id)
        {
            if (!productId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(productId));

            return m_productIds.TryGetValue(productId, out id);
        }

        protected abstract bool OnCheckAvailable();
        protected abstract bool OnCheckProcessingPurchase();
        protected abstract Task<bool> OnPurchaseStartAsync(GlobalId id);
        protected abstract Task OnPurchaseConfirmAsync(GlobalId id);
        protected abstract Task<IList<GlobalId>> OnGetPendingProductsAsync();
        protected abstract Task<IDictionary<string, IPurchaseProduct>> OnGetProductsAsync();
        protected abstract Task<TaskResult<string>> OnTryGetTransactionIdAsync(GlobalId id);

        protected virtual void OnProductAdded(GlobalId id, IPurchaseProductDescription description)
        {
        }

        protected virtual void OnProductRemoved(GlobalId id, IPurchaseProductDescription description)
        {
        }

        private void OnProductAdded(IProvider provider, GlobalId id, IPurchaseProductDescription entry)
        {
            m_productIds.Add(entry.Id, id);

            OnProductAdded(id, entry);
        }

        private void OnProductRemoved(IProvider provider, GlobalId id, IPurchaseProductDescription entry)
        {
            m_productIds.Remove(entry.Id);

            OnProductRemoved(id, entry);
        }
    }
}
