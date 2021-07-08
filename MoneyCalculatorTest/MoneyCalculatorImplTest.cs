using MoneyCalculator;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MoneyCalculatorTest
{
    [TestFixture]
    public class MoneyCalculatorImplTest
    {
        IMoneyCalculator _moneyCalculator;
        [SetUp]
        public void Setup()
        {
            _moneyCalculator = new MoneyCalculatorImpl();
        }

        [Test]
        public void MaxInvokedWithNullListShouldReturnNull()
        {
            IEnumerable<IMoney> monies = null;
            IMoney expectedResult = null;

            var actualResult = _moneyCalculator.Max(monies);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void MaxInvokedWithEmptyListShouldReturnNull()
        {
            IEnumerable<IMoney> monies = new List<IMoney>();
            IMoney expectedResult = null;

            var actualResult = _moneyCalculator.Max(monies);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void MaxInvokedWithMultipleCcyListShouldThrowArgumentException()
        {
            IEnumerable<IMoney> monies = new List<IMoney>()
            {
                new Money("GBP", 10),
                new Money("GBP", 20),
                new Money("EUR", 50)
            };

            var exception = Assert.Throws<ArgumentException>(() => _moneyCalculator.Max(monies));
            Assert.That(exception.Message, Is.EqualTo("All monies are not in the same currency."));
        }

        [Test]
        public void MaxInvokedWithSingleMoneyInListShouldReturnThePassedInValue()
        {
            IEnumerable<IMoney> monies = new List<IMoney>()
            {
                new Money("GBP", 10),
            };
            IMoney expectedResult = new Money("GBP", 10);

            var actualResult = _moneyCalculator.Max(monies);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void MaxInvokedWithMultipleMoneyInListShouldReturnMoneyWithMaxAmount()
        {
            IEnumerable<IMoney> monies = new List<IMoney>()
            {
                new Money("GBP", 10),
                new Money("GBP", 50),
                new Money("GBP", 20)
            };
            IMoney expectedResult = new Money("GBP", 50);

            var actualResult = _moneyCalculator.Max(monies);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void MaxInvokedWithMultipleMoneyWithSameAmountInListShouldReturnMoneyWithMaxAmount()
        {
            IEnumerable<IMoney> monies = new List<IMoney>()
            {
                new Money("GBP", 50),
                new Money("GBP", 100),
                new Money("GBP", 20),
                new Money("GBP", 100)
            };
            IMoney expectedResult = new Money("GBP", 100);

            var actualResult = _moneyCalculator.Max(monies);

            Assert.AreEqual(expectedResult, actualResult); ;
        }

        [Test]
        public void SumPerCurrencyInvokedWithNullListShouldReturnNull()
        {
            IEnumerable<IMoney> monies = null;
            IEnumerable<IMoney> expectedResult = null;

            var actualResult = _moneyCalculator.SumPerCurrency(monies);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void SumPerCurrencyInvokedWithEmptyListShouldReturnEmptyList()
        {
            IEnumerable<IMoney> monies = new List<IMoney>();
            IEnumerable<IMoney> expectedResult = new List<IMoney>();

            var actualResult = _moneyCalculator.SumPerCurrency(monies);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void SumPerCurrencyInvokedWithSingleCcyListShouldReturnOneRowWithSumOfCcyAmount()
        {
            IEnumerable<IMoney> monies = new List<IMoney>()
            {
                new Money("GBP", 10),
                new Money("GBP", 50),
                new Money("GBP", 20)
            };
            IEnumerable<IMoney> expectedResult = new List<IMoney>()
            {
                new Money("GBP", 80)
            };

            var actualResult = _moneyCalculator.SumPerCurrency(monies);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void SumPerCurrencyInvokedWithMultipleCcyListShouldReturnOneRowPerCcyWithSumOfCcyAmount()
        {
            IEnumerable<IMoney> monies = new List<IMoney>()
            {
                new Money("GBP", 100),
                new Money("USD", 10),
                new Money("GBP", 200),
                new Money("EUR", 1000),
                new Money("USD", 20),
                new Money("EUR", 2000)
            };
            IEnumerable<IMoney> expectedResult = new List<IMoney>()
            {
                new Money("GBP", 300),
                new Money("EUR", 3000),
                new Money("USD", 30)
            };

            var actualResult = _moneyCalculator.SumPerCurrency(monies);

            Assert.That(actualResult, Is.EquivalentTo(expectedResult));
        }
    }
}