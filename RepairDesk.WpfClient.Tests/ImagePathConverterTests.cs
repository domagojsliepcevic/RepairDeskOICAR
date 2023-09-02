using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using RepairDesk.WpfClient.Converters;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RepairDesk.WpfClient.Tests
{
    [TestFixture]
    public class ImagePathConverterTests
    {
        private ImagePathConverter _converter;

        [SetUp]
        public void Setup()
        {
            _converter = new ImagePathConverter();
        }


        [Test]
        // Test case 1: Convert returns null when input is null
        public void Convert_ReturnsNull_WhenInputIsNull()
        {
            // Arrange
            string input = null;

            // Act
            var result = _converter.Convert(input, typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        // Test case 2: Convert returns null when input is not a string
        public void Convert_ReturnsNull_WhenInputIsNotString()
        {
            // Arrange
            object input = 123;

            // Act
            var result = _converter.Convert(input, typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        // Test case 3: ConvertBack throws NotImplementedException
        public void ConvertBack_ThrowsNotImplementedException()
        {
            // Arrange
            object value = "some value";

            // Act & Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                _converter.ConvertBack(value, typeof(object), null, CultureInfo.CurrentCulture);
            });
        }
    }
}

