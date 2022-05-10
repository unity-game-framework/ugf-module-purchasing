using System;
using UnityEngine.Purchasing;

namespace UGF.Module.Purchasing.Runtime.Unity
{
    public static class PurchaseUnityUtility
    {
        public static PurchaseProduct GetProduct(Product unityProduct)
        {
            var price = new PurchasePrice((float)unityProduct.metadata.localizedPrice, unityProduct.metadata.localizedPriceString, unityProduct.metadata.isoCurrencyCode);
            var product = new PurchaseProduct(price);

            if (unityProduct.hasReceipt)
            {
                product.SetReceipt(unityProduct.receipt);
            }

            return product;
        }

        public static ProductType GetProductType(PurchaseProductType productType)
        {
            return TryGetProductType(productType, out ProductType value) ? value : throw new ArgumentException($"Product type not found by the specified type: '{productType}'.");
        }

        public static bool TryGetProductType(PurchaseProductType productType, out ProductType unityProductType)
        {
            switch (productType)
            {
                case PurchaseProductType.Consumable:
                {
                    unityProductType = ProductType.Consumable;
                    return true;
                }
                case PurchaseProductType.NonConsumable:
                {
                    unityProductType = ProductType.NonConsumable;
                    return true;
                }
                case PurchaseProductType.Subscription:
                {
                    unityProductType = ProductType.Subscription;
                    return true;
                }
            }

            unityProductType = default;
            return false;
        }
    }
}
