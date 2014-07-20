namespace CQRSShop.Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CQRSShop.Application;
    using CQRSShop.Infrastructure;

    using NUnit.Framework;

    public class TestBase
    {
        private readonly ICommandDispatcher commandDispatcher;

        private readonly IEventBus eventBus;

        public TestBase()
        {
            var engine = new Shop();

            this.commandDispatcher = engine.CommandDispatcher;
            this.eventBus = engine.EventBus;
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            IdGenerator.GenerateGuid = null;
        }

        protected void When(object command)
        {
            this.commandDispatcher.Dispatch(command);
        }

        protected void Then(params object[] expectedEvents)
        {
            var latestEvents = this.eventBus.GetAllUncommitted().ToList();
            this.eventBus.CommitAll();

            var expectedEventsList = expectedEvents.ToList();
            Assert.AreEqual(expectedEventsList.Count, latestEvents.Count);

            for (int i = 0; i < latestEvents.Count; i++)
            {
                Assert.AreEqual(expectedEvents[i], latestEvents[i]);
            }
        }

        protected void WhenThrows<TException>(object command) where TException : Exception
        {
            try
            {
                this.When(command);
                Assert.Fail("Expected exception " + typeof(TException));
            }
            catch (TException)
            {
            }
        }

        protected void Given(params object[] existingEvents)
        {
            foreach (var @event in existingEvents)
            {
                this.eventBus.Raise(@event);
            }
            
            this.eventBus.CommitAll();
        }
    }
}
