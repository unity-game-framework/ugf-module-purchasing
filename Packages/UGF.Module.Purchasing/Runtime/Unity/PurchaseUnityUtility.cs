using System;
using UnityEngine.Purchasing;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    public static class PurchaseUnityUtility
    {
        public static ProductType GetProductType(PurchaseProductType productType)
        {
            return TryGetProductType(productType, out ProductType value) ? value : throw new ArgumentException($"Product type not found by the specified type: '{productType}'.");
        }

        public static bool TryGetProductType(PurchaseProductType productType, out ProductType value)
        {
            switch (productType)
            {
                case PurchaseProductType.Consumable:
                {
                    value = ProductType.Consumable;
                    return true;
                }
                case PurchaseProductType.NonConsumable:
                {
                    value = ProductType.NonConsumable;
                    return true;
                }
                case PurchaseProductType.Subscription:
                {
                    value = ProductType.Subscription;
                    return true;
                }
            }

            value = default;
            return false;
        }
    }
}
