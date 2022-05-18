using System;

namespace UGF.Module.Purchasing.Runtime
{
    public class PurchasePrice : IPurchasePrice
    {
        public float Value { get; }
        public string Label { get; }
        public string IsoCode { get; }

        public PurchasePrice(float value, string label, string isoCode)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            if (string.IsNullOrEmpty(label)) throw new ArgumentException("Value cannot be null or empty.", nameof(label));
            if (string.IsNullOrEmpty(isoCode)) throw new ArgumentException("Value cannot be null or empty.", nameof(isoCode));

            Value = value;
            Label = label;
            IsoCode = isoCode;
        }
    }
}
