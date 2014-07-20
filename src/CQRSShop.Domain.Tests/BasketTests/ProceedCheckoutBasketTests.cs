using System;
using CQRSShop.Types;

using NUnit.Framework;

namespace CQRSShop.Domain.Tests.BasketTests
{
    using CQRSShop.Types.Commands;
    using CQRSShop.Types.Events;

    [TestFixture]
    public class ProceedCheckoutBasketTests : TestBase
    {
        [Test]
        public void GivenABasket_WhenCreatingABasketForCustomerX_ThenTheBasketShouldBeCreated()
        {
            var id = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            int discount = 0;
            Given(new BasketCreated(id, customerId, discount));
            When(new ProceedToCheckout(id));
            Then(new CustomerIsCheckingOutBasket(id));
        }
    }
}