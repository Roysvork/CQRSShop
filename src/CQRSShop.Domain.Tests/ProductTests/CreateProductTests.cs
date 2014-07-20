using System;
using CQRSShop.Types;

using NUnit.Framework;

namespace CQRSShop.Domain.Tests.ProductTests
{
    using CQRSShop.Types.Commands;
    using CQRSShop.Types.Events;
    using CQRSShop.Types.Exceptions;

    [TestFixture]
    public class CreateProductTests : TestBase
    {
        [TestCase("ball", 1000)]
        [TestCase("train", 10000)]
        [TestCase("universe", 999999)]
        public void WhenCreatingAProduct_TheProductShouldBeCreatedWithTheCorrectPrice(string productName, int price)
        {
            Guid id = Guid.NewGuid();
            When(new CreateProduct(id, productName, price));
            Then(new ProductCreated(id, productName, price));
        }

        [Test]
        public void GivenProductXExists_WhenCreatingAProductWithIdX_IShouldGetNotifiedThatTheProductAlreadyExists()
        {
            Guid id = Guid.NewGuid();
            Given(new ProductCreated(id, "Something I don't care about", 9999));
            WhenThrows<ProductAlreadyExistsException>(new CreateProduct(id, "Sugar", 999));
        }
    }
}