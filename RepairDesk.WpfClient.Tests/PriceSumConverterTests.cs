using NUnit.Framework;
using RepairDesk.ApiClient;
using RepairDesk.WpfClient.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace RepairDesk.WpfClient.Tests
{
    [TestFixture]
    public class PriceSumConverterTests
    {
        private PriceSumConverter _converter;

        [SetUp]
        public void Setup()
        {
            _converter = new PriceSumConverter();
        }

        [Test]
        // Test case 1: Convert returns the sum of prices when input is a collection
        public void Convert_ReturnsSumOfPrices_WhenInputIsCollection()
        {
            // Arrange
            var cartItems = new List<CartItemListModel>
            {
                new CartItemListModel { Price = 10 },
                new CartItemListModel { Price = 20 },
                new CartItemListModel { Price = 30 }
            };

            // Act
            var result = _converter.Convert(cartItems, typeof(decimal), null, CultureInfo.CurrentCulture);

            // Assert
            decimal expectedSum = cartItems.Sum(item => item.Price);
            Assert.That(result, Is.EqualTo(expectedSum));
        }

        [Test]
        // Test case 2: Convert returns 0 when input is null
        public void Convert_ReturnsZero_WhenInputIsNull()
        {
            // Arrange
            List<CartItemListModel> cartItems = null;

            // Act
            var result = _converter.Convert(cartItems, typeof(decimal), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        // Test case 3: Convert returns 0 when input is an empty collection
        public void Convert_ReturnsZero_WhenInputIsEmptyCollection()
        {
            // Arrange
            var cartItems = new List<CartItemListModel>();

            // Act
            var result = _converter.Convert(cartItems, typeof(decimal), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        // Test case 4: Convert returns 0 when input is not a collection
        public void Convert_ReturnsZero_WhenInputIsNotCollection()
        {
            // Arrange
            object input = 123;

            // Act
            var result = _converter.Convert(input, typeof(decimal), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        // Test case 5: ConvertBack throws NotSupportedException
        public void ConvertBack_ThrowsNotSupportedException()
        {
            // Arrange
            object value = 123;

            // Act & Assert
            Assert.Throws<NotSupportedException>(() =>
            {
                _converter.ConvertBack(value, typeof(object), null, CultureInfo.CurrentCulture);
            });
        }
    }
}


