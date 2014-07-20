using System;
using CQRSShop.Types;

using NUnit.Framework;

namespace CQRSShop.Domain.Tests.BasketTests
{
    using CQRSShop.Types.Commands;
    using CQRSShop.Types.Events;
    using CQRSShop.Types.Exceptions;

    [TestFixture]
    public class CheckoutBasketTests : TestBase
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void WhenTheUserCheckoutWithInvalidAddress_IShouldGetNotified(string street)
        {
            var address = street == null ? null : new Address(street);
            var id = Guid.NewGuid();
            Given(new BasketCreated(id, Guid.NewGuid(), 0));
            WhenThrows<MissingAddressException>(new CheckoutBasket(id, address));
        }

        [Test]
        public void WhenTheUserCheckoutWithAValidAddress_IShouldProceedToTheNextStep()
        {
            var address = new Address("Valid street");
            var id = Guid.NewGuid();
            Given(new BasketCreated(id, Guid.NewGuid(), 0));
            When(new CheckoutBasket(id, address));
            Then(new BasketCheckedOut(id, address));
        }
    }
}