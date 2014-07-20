namespace CQRSShop.Infrastructure
{
    using System.Collections.Generic;

    public interface IEventBus
    {
        IEnumerable<object> Get<TEntity>(object eventKey);

        IEnumerable<object> GetAll();

        IEnumerable<object> GetAllUncommitted();

        void Raise(object domainEvent);

        void Commit<TEntity>(object eventKey);

        void CommitAll();

        void Publish();
    }
}