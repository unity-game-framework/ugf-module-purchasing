using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Purchasing/Purchase Unity Module", order = 2000)]
    public class PurchaseUnityModuleAsset : ApplicationModuleAsset<PurchaseUnityModule, PurchaseUnityModuleDescription>
    {
        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new PurchaseUnityModuleDescription
            {
                RegisterType = typeof(IPurchaseModule)
            };

            return description;
        }

        protected override PurchaseUnityModule OnBuild(PurchaseUnityModuleDescription description, IApplication application)
        {
            return new PurchaseUnityModule(description, application);
        }
    }
}
