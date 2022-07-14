using System;

namespace UGF.Module.Purchasing.Runtime
{
    public readonly struct PurchaseProductId : IComparable<PurchaseProductId>
    {
        public string Value { get; }

        public PurchaseProductId(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            Value = value;
        }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(Value);
        }

        public bool Equals(PurchaseProductId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is PurchaseProductId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }

        public int CompareTo(PurchaseProductId other)
        {
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
