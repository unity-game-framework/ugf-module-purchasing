using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Purchasing/Purchase Unity Module", order = 2000)]
    public class PurchaseUnityModuleAsset
#if UGF_MODULE_PURCHASING_PURCHASING_INSTALLED
        : ApplicationModuleAsset<PurchaseUnityModule, PurchaseUnityModuleDescription>
#else
        : ApplicationModuleAsset<IApplicationModule, PurchaseUnityModuleDescription>
#endif
    {
        [SerializeField] private List<AssetIdReference<PurchaseProductDescriptionAsset>> m_products = new List<AssetIdReference<PurchaseProductDescriptionAsset>>();

        public List<AssetIdReference<PurchaseProductDescriptionAsset>> Products { get { return m_products; } }

        protected override PurchaseUnityModuleDescription OnBuildDescription()
        {
            var products = new Dictionary<GlobalId, IPurchaseProductDescription>();

            for (int i = 0; i < m_products.Count; i++)
            {
                AssetIdReference<PurchaseProductDescriptionAsset> reference = m_products[i];

                products.Add(reference.Guid, reference.Asset.Build());
            }

            return new PurchaseUnityModuleDescription(products);
        }

#if UGF_MODULE_PURCHASING_PURCHASING_INSTALLED
        protected override PurchaseUnityModule OnBuild(PurchaseUnityModuleDescription description, IApplication application)
        {
            return new PurchaseUnityModule(description, application);
        }
#else
        protected override IApplicationModule OnBuild(PurchaseUnityModuleDescription description, IApplication application)
        {
            throw new NotSupportedException("Purchase Unity Module: Purchasing package required.");
        }
#endif
    }
}
