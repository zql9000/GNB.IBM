using GNB.IBM.Application.Exceptions;
using GNB.IBM.Application.Support;
using GNB.IBM.Core.Entities;
using System.Collections.Generic;
using Xunit;

namespace GNB.IBM.Application.Tests.Support
{
    public class CurrencyConversionTests
    {
        [Fact]
        public void Convert_WhenFromAndToAreTheSame_ThenReturnsTheSameAmount()
        {
            // Arrange
            var from = "A";
            var to = "A";
            var amount = 1.1f;
            IEnumerable<ConversionRate> stubConvertionRateModels = new List<ConversionRate>();
            var currencyConversion = new CurrencyConversion(stubConvertionRateModels);
            var expected = amount;

            // Act
            float amountConverted = currencyConversion.Convert(from, to, amount);

            // Assert
            Assert.Equal(expected, amountConverted);
        }

        [Fact]
        public void Convert_WhenFromAndToAreDirectAvailableInTheConvertionRates_ThenReturnsTheAmountMultipliedAndRounded()
        {
            // Arrange
            var from = "A";
            var to = "B";
            var amount = 1.1f;
            var rate = 1.1f;
            IEnumerable<ConversionRate> stubConvertionRateModels = new List<ConversionRate>
            {
                new ConversionRate { From = from, To = to, Rate = rate },
            };
            var currencyConversion = new CurrencyConversion(stubConvertionRateModels);
            float expected = amount * rate;

            // Act
            float amountConverted = currencyConversion.Convert(from, to, amount);

            // Assert
            Assert.Equal(expected, amountConverted);
        }

        [Fact]
        public void Convert_WhenFromAndToAreIndirectAvailableInTheConvertionRates_ThenReturnsTheAmountMultipliedAndRoundedNTimes()
        {
            // Arrange
            var from = "A";
            var to = "C";
            var amount = 1.1f;
            IEnumerable<ConversionRate> stubConvertionRateModels = new List<ConversionRate>
            {
                new ConversionRate { From = "A", To = "B", Rate = 1.359f },
                new ConversionRate { From = "B", To = "A", Rate = 0.736f },
                new ConversionRate { From = "B", To = "C", Rate = 1.366f },
                new ConversionRate { From = "C", To = "B", Rate = 0.732f }
            };
            var currencyConversion = new CurrencyConversion(stubConvertionRateModels);
            float expected = 2.04f;

            // Act
            float amountConverted = currencyConversion.Convert(from, to, amount);

            // Assert
            Assert.Equal(expected, amountConverted);
        }

        [Fact]
        public void Convert_WhenFromAndToAreNotAvailableInTheConvertionRates_ThenThrowsAnException()
        {
            // Arrange
            var from = "A";
            var to = "B";
            var amount = 1.1f;
            IEnumerable<ConversionRate> stubConvertionRateModels = new List<ConversionRate>();
            var currencyConversion = new CurrencyConversion(stubConvertionRateModels);

            // Act
            // Assert
            Assert.Throws<ApplicationException>(() => currencyConversion.Convert(from, to, amount));

        }
    }
}
