using System;

using NUnit.Framework;

namespace CQRSShop.Domain.Tests.BasketTests
{
    using System.Collections.Generic;

    using CQRSShop.Types;
    using CQRSShop.Types.Commands;
    using CQRSShop.Types.Events;
    using CQRSShop.Types.Exceptions;

    [TestFixture]
    public class MakePaymentTests : TestBase
    {
        [TestCase(100, 101)]
        [TestCase(100, 99)]
        [TestCase(100, 91)]
        [TestCase(100, 89)]
        public void WhenNotPayingTheExpectedAmount_IShouldGetNotified(int productPrice, int payment)
        {
            var id = Guid.NewGuid();
            var existingOrderLine = new OrderLine(Guid.NewGuid(), "", productPrice, productPrice, 1);
            Given(new BasketCreated(id, Guid.NewGuid(), 0),
                new ItemAdded(id, existingOrderLine));
            WhenThrows<UnexpectedPaymentException>(new MakePayment(id, payment));
        }

        [TestCase(100, 101, 101)]
        [TestCase(100, 80, 80)]
        public void WhenPayingTheExpectedAmount_ThenANewOrderShouldBeCreatedFromTheResult(int productPrice, int discountPrice, int payment)
        {
            var id = Guid.NewGuid();
            int dontCare = 0;
            var orderId = Guid.NewGuid();
            IdGenerator.GenerateGuid = () => orderId;
            var existingOrderLine = new OrderLine(Guid.NewGuid(), "Ball", productPrice, discountPrice, 1);
            Given(new BasketCreated(id, Guid.NewGuid(), dontCare),
                new ItemAdded(id, existingOrderLine));
            When(new MakePayment(id, payment));

            var items = new List<OrderLine> { existingOrderLine };
            Then(new OrderCreated(orderId, id, items),
                new OrderApproved(orderId));
        }
    }
}