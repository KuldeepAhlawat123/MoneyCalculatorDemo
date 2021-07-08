
namespace MoneyCalculator
{
    /// <summary>
    /// An amount of money in a particular currency.
    /// </summary>
    public class Money : IMoney
    {
        public Money(string ccy, decimal amount)
        {
            Currency = ccy;
            Amount = amount;
        }
        /// <summary>
        /// The amount of money this instance represents.
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// The ISO currency code of this instance.
        /// </summary>
        public string Currency { get; }

        public override string ToString()
        {
            return $"{Currency}{Amount}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var money = obj as Money;
            if (money == null)
            {
                return false;
            }

            return (Currency == money.Currency) && (Amount == money.Amount);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Amount.GetHashCode();
                if (!string.IsNullOrEmpty(Currency))
                    hashCode = (hashCode * 397) ^ Currency.GetHashCode();
                return hashCode;
            }
        }
    }
}
