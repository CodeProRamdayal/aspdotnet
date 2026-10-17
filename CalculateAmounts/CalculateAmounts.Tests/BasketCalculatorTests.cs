using System;
using System.Collections.Generic;
using CalculateAmounts.Shopping;
using Xunit;

namespace CalculateAmounts.Tests
{
    public class BasketCalculatorTests
    {
        private readonly BasketCalculator _calculator = new();

        [Fact]
        public void EmptyBasket_ReturnsZero()
        {
            var result = _calculator.CalculateTotal(Array.Empty<string>());
            Assert.Equal(0, result);
        }

        [Fact]
        public void NullBasket_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _calculator.CalculateTotal(null));
        }

        [Fact]
        public void UnknownItem_ThrowsArgumentException()
        {
            var basket = new[] { "Apple", "Chocolate" };
            var ex = Assert.Throws<ArgumentException>(() => _calculator.CalculateTotal(basket));
            Assert.Contains("Unknown item", ex.Message);
        }

        [Fact]
        public void NullItemEntry_ThrowsArgumentException()
        {
            var basket = new string[] { "Apple", null };
            Assert.Throws<ArgumentException>(() => _calculator.CalculateTotal(basket));
        }

        [Theory]
        [InlineData(new object[] { new string[] { "Apple" }, 35 })]
        [InlineData(new object[] { new string[] { "Banana" }, 20 })]
        [InlineData(new object[] { new string[] { "Apple", "Apple", "Banana" }, 90 })]
        public void SingleAndMultipleItems_CalculatedCorrectly(string[] basket, int expected)
        {
            var result = _calculator.CalculateTotal(basket);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 50)]
        [InlineData(2, 50)]
        [InlineData(3, 100)]
        [InlineData(4, 100)]
        public void Melon_BuyOneGetOneFree_Works(int count, int expected)
        {
            var basket = new List<string>();
            for (int i = 0; i < count; i++) basket.Add("Melon");
            var result = _calculator.CalculateTotal(basket);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 15)]
        [InlineData(2, 30)]
        [InlineData(3, 30)]
        [InlineData(4, 45)]
        [InlineData(6, 60)]
        public void Lime_ThreeForTwo_Works(int count, int expected)
        {
            var basket = new List<string>();
            for (int i = 0; i < count; i++) basket.Add("Lime");
            var result = _calculator.CalculateTotal(basket);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MixedBasket_WithPromotions_CalculatesCorrectTotal()
        {
            var basket = new[] { "Apple", "Apple", "Banana", "Melon", "Melon", "Melon", "Lime", "Lime", "Lime", "Lime" };
            var result = _calculator.CalculateTotal(basket);
            Assert.Equal(235, result);
        }
    }
}
