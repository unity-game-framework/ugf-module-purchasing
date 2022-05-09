using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Purchasing/Purchase Module Unity", order = 2000)]
    public class PurchaseModuleUnityAsset : ApplicationModuleAsset<PurchaseModuleUnity, PurchaseModuleUnityDescription>
    {
        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new PurchaseModuleUnityDescription
            {
                RegisterType = typeof(IPurchaseModule)
            };

            return description;
        }

        protected override PurchaseModuleUnity OnBuild(PurchaseModuleUnityDescription description, IApplication application)
        {
            return new PurchaseModuleUnity(description, application);
        }
    }
}
