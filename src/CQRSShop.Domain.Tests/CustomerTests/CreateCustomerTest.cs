using System;
using CQRSShop.Types;

using NUnit.Framework;

namespace CQRSShop.Domain.Tests.CustomerTests
{
    using CQRSShop.Types.Commands;
    using CQRSShop.Types.Events;
    using CQRSShop.Types.Exceptions;

    [TestFixture]
    public class CreateCustomerTest : TestBase
    {
        [Test]
        public void WhenCreatingTheCustomer_TheCustomerShouldBeCreatedWithTheRightName()
        {
            Guid id = Guid.NewGuid();
            When(new CreateCustomer(id, "Tomas"));
            Then(new CustomerCreated(id, "Tomas"));
        }

        [Test]
        public void GivenAUserWithIdXExists_WhenCreatingACustomerWithIdX_IShouldGetNotifiedThatTheUserAlreadyExists()
        {
            Guid id = Guid.NewGuid();
            Given(new CustomerCreated(id, "Something I don't care about"));
            WhenThrows<CustomerAlreadyExistsException>(new CreateCustomer(id, "Tomas"));
        }
    }
}