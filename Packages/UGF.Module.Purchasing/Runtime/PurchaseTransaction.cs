using System;

namespace UGF.Module.Purchasing.Runtime
{
    public class PurchaseTransaction : IPurchaseTransaction
    {
        public string Id { get; }
        public string Receipt { get { return HasReceipt ? m_receipt : throw new ArgumentException("Value not specified."); } }
        public bool HasReceipt { get { return !string.IsNullOrEmpty(m_receipt); } }

        private string m_receipt;

        public PurchaseTransaction(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            Id = id;
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
