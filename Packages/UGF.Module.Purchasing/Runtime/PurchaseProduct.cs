using System;

namespace UGF.Module.Purchasing.Runtime
{
    public class PurchaseProduct : IPurchaseProduct
    {
        public bool Available { get; set; }
        public IPurchasePrice Price { get; }
        public string Receipt { get { return HasReceipt ? m_receipt : throw new ArgumentException("Value not specified."); } }
        public bool HasReceipt { get { return !string.IsNullOrEmpty(m_receipt); } }

        private string m_receipt;

        public PurchaseProduct(IPurchasePrice price)
        {
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }

        public void SetReceipt(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            m_receipt = value;
        }

        public void ClearReceipt()
        {
            m_receipt = default;
        }
    }
}
