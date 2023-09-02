using RepairDesk.BlazorClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDesk.BlazorClient.Tests
{
    [TestFixture]
    public class ProductSearchServiceTests
    {
        private ProductSearchService _searchService;

        [SetUp]
        public void Setup()
        {
            _searchService = new ProductSearchService();
        }

        // Test 1: Set properties in ProductSearchService
        [Test]
        public void ProductSearchService_SetProperties()
        {
            _searchService.Name = "Product Name";
            _searchService.Description = "Product Description";
            _searchService.Text = "Product Text";
            _searchService.CategoryId = 1;
            _searchService.Brand = "Product Brand";
            _searchService.PriceFrom = 10.0m;
            _searchService.PriceTo = 100.0m;

            Assert.That(_searchService.Name, Is.EqualTo("Product Name"));
            Assert.That(_searchService.Description, Is.EqualTo("Product Description"));
            Assert.That(_searchService.Text, Is.EqualTo("Product Text"));
            Assert.That(_searchService.CategoryId, Is.EqualTo(1));
            Assert.That(_searchService.Brand, Is.EqualTo("Product Brand"));
            Assert.That(_searchService.PriceFrom, Is.EqualTo(10.0m));
            Assert.That(_searchService.PriceTo, Is.EqualTo(100.0m));
        }

        // Test 2: Check default values in ProductSearchService
        [Test]
        public void ProductSearchService_DefaultValues()
        {
            // Test checking default values in ProductSearchService
            Assert.IsNull(_searchService.Name);
            Assert.IsNull(_searchService.Description);
            Assert.IsNull(_searchService.Text);
            Assert.IsNull(_searchService.CategoryId);
            Assert.IsNull(_searchService.Brand);
            Assert.IsNull(_searchService.PriceFrom);
            Assert.IsNull(_searchService.PriceTo);
        }

        // Test 3: Reset ProductSearchService
        [Test]
        public void ProductSearchService_ClearProperties()
        {
            // Test clearing properties in ProductSearchService
            _searchService.Name = "Product Name";
            _searchService.Description = "Product Description";
            _searchService.Text = "Product Text";
            _searchService.CategoryId = 1;
            _searchService.Brand = "Product Brand";
            _searchService.PriceFrom = 10.0m;
            _searchService.PriceTo = 100.0m;

            _searchService.Reset();

            Assert.IsNull(_searchService.Name);
            Assert.IsNull(_searchService.Description);
            Assert.IsNull(_searchService.Text);
            Assert.IsNull(_searchService.CategoryId);
            Assert.IsNull(_searchService.Brand);
            Assert.IsNull(_searchService.PriceFrom);
            Assert.IsNull(_searchService.PriceTo);
        }

        // Test 4: Generate query string with all properties set
        [Test]
        public void ProductSearchService_GenerateQueryString_WithAllPropertiesSet()
        {
            var searchService = new ProductSearchService();
            searchService.Name = "Product Name";
            searchService.Description = "Product Description";
            searchService.Text = "Product Text";
            searchService.CategoryId = 1;
            searchService.Brand = "Product Brand";
            searchService.PriceFrom = 10.0m;
            searchService.PriceTo = 100.0m;

            var expectedQueryString = "categoryid=1&brand=Product%20Brand&pricefrom=10.00&priceto=100.00";
            var actualQueryString = searchService.GenerateQueryString();

            Assert.That(actualQueryString, Is.EqualTo(expectedQueryString));
        }

        // Test 5: Generate query string with some properties set
        [Test]
        public void ProductSearchService_GenerateQueryString_WithSomePropertiesSet()
        {
            var searchService = new ProductSearchService();
            searchService.Name = "Product Name";
            searchService.Description = "Product Description";

            var expectedQueryString = "categoryid=&brand=&pricefrom=&priceto=";
            var actualQueryString = searchService.GenerateQueryString();

            Assert.That(actualQueryString, Is.EqualTo(expectedQueryString));
        }


    }
}
