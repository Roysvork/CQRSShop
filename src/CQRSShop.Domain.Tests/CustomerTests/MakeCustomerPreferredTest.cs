using System;
using CQRSShop.Types;

using NUnit.Framework;

namespace CQRSShop.Domain.Tests.CustomerTests
{
    using CQRSShop.Types.Commands;
    using CQRSShop.Types.Events;

    [TestFixture]
    public class MarkCustomerAsPreferredTest : TestBase
    {
        [TestCase(25)]
        [TestCase(50)]
        [TestCase(70)]
        public void GivenTheUserExists_WhenMarkingCustomerAsPreferred_ThenTheCustomerShouldBePreferred(int discount)
        {
            Guid id = Guid.NewGuid();
            Given(new CustomerCreated(id, "Superman"));
            When(new MarkCustomerAsPreferred(id, discount));
            Then(new CustomerMarkedAsPreferred(id, discount));
        }
    }
}