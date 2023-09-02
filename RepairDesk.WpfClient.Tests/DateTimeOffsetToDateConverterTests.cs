using NUnit.Framework;
using RepairDesk.WpfClient.Converters;
using System;
using System.Globalization;
using System.Windows.Data;

namespace RepairDesk.WpfClient.Tests
{
    [TestFixture]
    public class DateTimeOffsetToDateConverterTests
    {
        private DateTimeOffsetToDateConverter _converter;

        [SetUp]
        public void Setup()
        {
            _converter = new DateTimeOffsetToDateConverter();
        }

        [Test]
        // Test case 1: Convert returns the DateTime value of the DateTimeOffset input
        public void Convert_ReturnsDateTimeValue_WhenInputIsDateTimeOffset()
        {
            // Arrange
            DateTimeOffset input = new DateTimeOffset(new DateTime(2022, 1, 1));

            // Act
            var result = _converter.Convert(input, typeof(DateTime), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.That(result, Is.EqualTo(input.DateTime));
        }

        [Test]
        // Test case 2: Convert returns Binding.DoNothing when input is not a DateTimeOffset
        public void Convert_ReturnsDoNothing_WhenInputIsNotDateTimeOffset()
        {
            // Arrange
            object input = "invalid";

            // Act
            var result = _converter.Convert(input, typeof(DateTime), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.That(result, Is.EqualTo(Binding.DoNothing));
        }

        [Test]
        // Test case 3: ConvertBack returns a DateTimeOffset instance when input is DateTime
        public void ConvertBack_ReturnsDateTimeOffset_WhenInputIsDateTime()
        {
            // Arrange
            DateTime input = new DateTime(2022, 1, 1);

            // Act
            var result = _converter.ConvertBack(input, typeof(DateTimeOffset), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.That(result, Is.EqualTo(new DateTimeOffset(input)));
        }

        [Test]
        // Test case 4: ConvertBack returns Binding.DoNothing when input is not a DateTime
        public void ConvertBack_ReturnsDoNothing_WhenInputIsNotDateTime()
        {
            // Arrange
            object input = "invalid";

            // Act
            var result = _converter.ConvertBack(input, typeof(DateTimeOffset), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.That(result, Is.EqualTo(Binding.DoNothing));
        }

    }
}

