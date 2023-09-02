using RepairDesk.BlazorClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDesk.BlazorClient.Tests
{
    [TestFixture]
    public class PaginationServiceTests
    {
        private PaginationService _paginationService;

        [SetUp]
        public void Setup()
        {
            _paginationService = new PaginationService();
        }

        // Test 1: PaginationService initializes with null current pages for all properties
        [Test]
        public void PaginationService_InitializesWithNullCurrentPages()
        {
            Assert.IsNull(_paginationService.ProductInventoryCurrentPage);
            Assert.IsNull(_paginationService.AdminProductsCurrentPage);
            Assert.IsNull(_paginationService.AdminCategoriesCurrentPage);
            Assert.IsNull(_paginationService.AdminPOSesCurrentPage);
            Assert.IsNull(_paginationService.AdminRepairsCurrentPage);
            Assert.IsNull(_paginationService.AdminOrdersCurrentPage);
        }

        // Test 2: SetProductInventoryCurrentPage sets the correct value
        [Test]
        public void PaginationService_SetProductInventoryCurrentPage_SetsCorrectValue()
        {
            int page = 1;
            _paginationService.ProductInventoryCurrentPage = page;
            Assert.That(_paginationService.ProductInventoryCurrentPage, Is.EqualTo(page));
        }

        // Test 3: SetAdminProductsCurrentPage sets the correct value
        [Test]
        public void PaginationService_SetAdminProductsCurrentPage_SetsCorrectValue()
        {
            int page = 2;
            _paginationService.AdminProductsCurrentPage = page;
            Assert.That(_paginationService.AdminProductsCurrentPage, Is.EqualTo(page));
        }

        // Test 4: SetAdminCategoriesCurrentPage sets the correct value
        [Test]
        public void PaginationService_SetAdminCategoriesCurrentPage_SetsCorrectValue()
        {
            int page = 3;
            _paginationService.AdminCategoriesCurrentPage = page;
            Assert.That(_paginationService.AdminCategoriesCurrentPage, Is.EqualTo(page));
        }

        // Test 5: SetAdminPOSesCurrentPage sets the correct value
        [Test]
        public void PaginationService_SetAdminPOSesCurrentPage_SetsCorrectValue()
        {
            int page = 4;
            _paginationService.AdminPOSesCurrentPage = page;
            Assert.That(_paginationService.AdminPOSesCurrentPage, Is.EqualTo(page));
        }
    }

}
