using UGF.Builder.Runtime;
using UnityEngine;

namespace UGF.Module.Purchasing.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Purchasing/Purchase Product Description", order = 2000)]
    public class PurchaseProductDescriptionAsset : BuilderAsset<IPurchaseProductDescription>
    {
        [SerializeField] private string m_id;
        [SerializeField] private PurchaseProductType m_type = PurchaseProductType.Consumable;

        public string ID { get { return m_id; } set { m_id = value; } }
        public PurchaseProductType Type { get { return m_type; } set { m_type = value; } }

        protected override IPurchaseProductDescription OnBuild()
        {
            var description = new PurchaseProductDescription
            {
                Id = m_id,
                Type = m_type
            };

            return description;
        }
    }
}
