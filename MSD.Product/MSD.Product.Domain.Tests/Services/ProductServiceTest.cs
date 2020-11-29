using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSD.Product.Domain.Dtos.ProductDtos;
using MSD.Product.Domain.Interfaces.Repositories;
using MSD.Product.Domain.Services;
using MSD.Product.Infra.Api.Dtos;
using MSD.Product.Infra.Warning;
using System;
using System.Threading.Tasks;

namespace MSD.Product.Domain.Tests.Services
{
    [TestClass]
    public class ProductServiceTest
    {
        private WarningManagement warningManagement;

        private IProductRepositoryDb productRepositoryDb;
        private IProductRepositoryApi productRepositoryApi;

        private ProductService productService;

        [TestInitialize]
        public void Setup()
        {
            warningManagement = new WarningManagement();

            var productRepositoryDbMock = new Mock<IProductRepositoryDb>();
            var productRepositoryApiMock = new Mock<IProductRepositoryApi>();

            productRepositoryApiMock
                .Setup(e => e.GetByExternalIdAsync(It.IsAny<string>()))
                .Returns(
                    Task.FromResult(
                        new ApiResult<ProductDto>(
                            new ProductDto("test name", "test external id", DateTime.UtcNow),
                            "url test"
                        )
                    )
                );

            productRepositoryDb = productRepositoryDbMock.Object;
            productRepositoryApi = productRepositoryApiMock.Object;

            productService = new ProductService(warningManagement, productRepositoryApi, productRepositoryDb);
        }

        [TestMethod]
        public async Task ShouldNotEmitWarningOnCorrectPriceSet()
        {
            var dto = new PriceDto
            {
                Price = 2.2m,
                ProductExternalId = "test id"
            };

            await productService.SetPriceAsync(dto);

            Assert.IsFalse(warningManagement.Any(), $"Should Not Emit Warning On Correct Price Set");
        }

        [TestMethod]
        public async Task ShouldNotSetPriceEqualsZero()
        {
            var dto = new PriceDto
            {
                Price = 0,
                ProductExternalId = "test id"
            };

            await productService.SetPriceAsync(dto);

            Assert.IsTrue(warningManagement.Any(), $"Should Not Set Price Equals Zero to Product");

            warningManagement.List().ForEach(warning => {
                Assert.IsNotNull(warning, $"Should Not Set Price Equals Zero to Product - item");
                Assert.IsNotNull(warning.Message, $"Should Not Set Price Equals Zero to Product - Message");
                Assert.IsTrue(warning.Message.Contains("Invalid price!"), $"Should Not Set Price Equals Zero to Product - Correct Message");
            });
        }

        [TestMethod]
        public async Task ShouldNotSetPriceLessThanZero()
        {
            var dto = new PriceDto
            {
                Price = -5,
                ProductExternalId = "test id"
            };

            await productService.SetPriceAsync(dto);

            Assert.IsTrue(warningManagement.Any(), $"Should Not Set Price Less Than Zero to Product");

            warningManagement.List().ForEach(warning => {
                Assert.IsNotNull(warning, $"Should Not Set Price Less Than Zero to Product - item");
                Assert.IsNotNull(warning.Message, $"Should Not Set Price Less Than Zero to Product - Message");
                Assert.IsTrue(warning.Message.Contains("Invalid price!"), $"Should Not Set Price Less Than Zero to Product - Correct Message");
            });
        }
    }
}
