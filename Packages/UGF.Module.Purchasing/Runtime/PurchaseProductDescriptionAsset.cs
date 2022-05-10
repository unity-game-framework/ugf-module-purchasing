using UGF.Builder.Runtime;
using UnityEngine;

namespace UGF.Module.Purchasing.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Purchasing/Purchase Product Description", order = 2000)]
    public class PurchaseProductDescriptionAsset : BuilderAsset<PurchaseProductDescription>
    {
        [SerializeField] private string m_storeId;
        [SerializeField] private PurchaseProductType m_productType = PurchaseProductType.Consumable;

        public string StoreId { get { return m_storeId; } set { m_storeId = value; } }
        public PurchaseProductType ProductType { get { return m_productType; } set { m_productType = value; } }

        protected override PurchaseProductDescription OnBuild()
        {
            var description = new PurchaseProductDescription
            {
                StoreId = m_storeId,
                ProductType = m_productType
            };

            return description;
        }
    }
}
