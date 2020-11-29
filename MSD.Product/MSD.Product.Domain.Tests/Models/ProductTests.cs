using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MSD.Product.Domain.Tests.Models
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void ShouldNotEmitWarningOnCorrectPriceSet()
        {
            var entity = new Domain.Models.Product("test name", "test id", DateTime.UtcNow);
            var warning = entity.SetPrice(1.2m);

            Assert.IsNull(warning, $"Should Not Emit Warning On Correct Price Set");
        }

        [TestMethod]
        public void ShouldNotSetPriceEqualsZero()
        {
            var entity = new Domain.Models.Product("test name", "test id", DateTime.UtcNow);
            var warning = entity.SetPrice(0);

            Assert.IsNotNull(warning, $"Should Not Set Price Equals Zero to Product");
            Assert.IsNotNull(warning.Message, $"Should Not Set Price Equals Zero to Product - Message");
            Assert.IsTrue(warning.Message.Contains("Invalid price!"), $"Should Not Set Price Equals Zero to Product - Correct Message");
        }

        [TestMethod]
        public void ShouldNotSetPriceLessThanZero()
        {
            var entity = new Domain.Models.Product("test name", "test id", DateTime.UtcNow);
            var warning = entity.SetPrice(-1.1m);

            Assert.IsNotNull(warning, $"Should Not Set Price Less Than Zero to Product");
            Assert.IsNotNull(warning.Message, $"Should Not Set Price Less Than Zero to Product - Message");
            Assert.IsTrue(warning.Message.Contains("Invalid price!"), $"Should Not Set Price Less Than Zero to Product - Correct Message");
        }
    }
}
