using NUnit.Framework;
using RepairDesk.WpfClient.Converters;
using System.Globalization;
using System.Windows;


namespace RepairDesk.WpfClient.Tests
{

    [TestFixture]
    public class BooleanToVisibilityConverterTests
    {
        private BooleanToVisibilityConverter _converter;

        [SetUp]
        public void Setup()
        {
            _converter = new BooleanToVisibilityConverter();
        }

        [Test]
        // Test case 1: Convert returns Visibility.Visible when input is true
        public void Convert_ReturnsVisible_WhenInputIsTrue()
        {
            // Arrange
            bool input = true;

            // Act
            var result = _converter.Convert(input, typeof(bool), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.That(result, Is.EqualTo(Visibility.Visible));
        }

        [Test]
        // Test case 2: Convert returns Visibility.Collapsed when input is false
        public void Convert_ReturnsCollapsed_WhenInputIsFalse()
        {
            // Arrange
            bool input = false;

            // Act
            var result = _converter.Convert(input, typeof(bool), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.That(result, Is.EqualTo(Visibility.Collapsed));
        }

        [Test]
        // Test case 3: Convert returns Visibility.Collapsed when input is not a boolean value
        public void Convert_ReturnsCollapsed_WhenInputIsNotBoolean()
        {
            // Arrange
            object input = "invalid";

            // Act
            var result = _converter.Convert(input, typeof(bool), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.That(result, Is.EqualTo(Visibility.Collapsed));
        }

        [Test]
        // Test case 4: ConvertBack throws NotImplementedException
        public void ConvertBack_ThrowsNotImplementedException()
        {
            // Arrange
            var value = Visibility.Visible;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() =>
                _converter.ConvertBack(value, typeof(Visibility), null, CultureInfo.CurrentCulture));
        }
    }

}
