using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyCalculator
{
    /// <summary>
    /// Some fun things to do with money.
    /// </summary>
    public class MoneyCalculatorImpl : IMoneyCalculator
    {
        /// <summary>
        /// Find the largest amount of money.
        /// </summary>
        /// <returns>The <see cref="IMoney"/> instance having the largest amount.</returns>
        /// <exception cref="ArgumentException">All monies are not in the same currency.</exception>
        /// <example>{GBP10, GBP20, GBP50} => {GBP50}</example>
        /// <example>{GBP10, GBP20, EUR50} => exception</example>
        public IMoney Max(IEnumerable<IMoney> monies)
        {
            if (monies == null || !monies.Any())
                return null;

            var ccy = monies.First().Currency;
            if (monies.Any(money => !string.Equals(money.Currency, ccy, StringComparison.InvariantCulture)))
            {
                throw new ArgumentException("All monies are not in the same currency.");
            }

            return monies.OrderByDescending(money => money.Amount).First();
        }

        /// <summary>
        /// Return one <see cref="IMoney"/> per currency with the sum of all monies of the same currency.
        /// </summary>
        /// <example>{GBP10, GBP20, GBP50} => {GBP80}</example>
        /// <example>{GBP10, GBP20, EUR50} => {GBP30, EUR50}</example>
        /// <example>{GBP10, USD20, EUR50} => {GBP10, USD20, EUR50}</example>
        public IEnumerable<IMoney> SumPerCurrency(IEnumerable<IMoney> monies)
        {
            if (monies == null)
                return null;

            if (!monies.Any())
                return new List<IMoney>();

            return monies.GroupBy(money => money.Currency)
                        .Select(item => { return new Money(item.Key, item.Sum(i => i.Amount)); });
        }
    }
}
