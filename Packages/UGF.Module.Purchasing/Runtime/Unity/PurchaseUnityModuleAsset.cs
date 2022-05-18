using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Purchasing/Purchase Unity Module", order = 2000)]
    public class PurchaseUnityModuleAsset : ApplicationModuleAsset<PurchaseUnityModule, PurchaseUnityModuleDescription>
    {
        [SerializeField] private List<AssetReference<PurchaseProductDescriptionAsset>> m_products = new List<AssetReference<PurchaseProductDescriptionAsset>>();

        public List<AssetReference<PurchaseProductDescriptionAsset>> Products { get { return m_products; } }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new PurchaseUnityModuleDescription
            {
                RegisterType = typeof(IPurchaseModule)
            };

            for (int i = 0; i < m_products.Count; i++)
            {
                AssetReference<PurchaseProductDescriptionAsset> reference = m_products[i];

                description.Products.Add(reference.Guid, reference.Asset);
            }

            return description;
        }

        protected override PurchaseUnityModule OnBuild(PurchaseUnityModuleDescription description, IApplication application)
        {
            return new PurchaseUnityModule(description, application);
        }
    }
}
