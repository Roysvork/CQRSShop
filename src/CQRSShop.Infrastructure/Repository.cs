namespace CQRSShop.Infrastructure
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using CQRSShop.Infrastructure.Exceptions;

    public interface IRepository<out TEntity>
    {
        TEntity GetById(object id);
    }

    public class Repository<TEntity> : IRepository<TEntity>
    {
        private readonly IEventBus eventBus;

        private readonly IDictionary<Type, EntityMapping> entityMappings;

        public Repository(IEventBus eventBus, IDictionary<Type, EntityMapping> entityMappings)
        {
            this.eventBus = eventBus;
            this.entityMappings = entityMappings;
        }

        public void Save(object key)
        {
            this.eventBus.Commit<TEntity>(key);
            this.eventBus.Publish();
        }

        public TEntity GetById(object key)
        {
            var entity = (TEntity)FormatterServices.GetUninitializedObject(typeof(TEntity));
            var events = this.eventBus.Get<TEntity>(key).ToList();

            if (!events.Any())
            {
                throw new EntityNotFoundException(string.Format("{0} with key {1} was not found", typeof(TEntity), key));
            }

            foreach (var @event in events)
            {
                var eventType = @event.GetType();
                if (this.entityMappings.ContainsKey(eventType))
                {
                    var mapping = this.entityMappings[eventType];
                    mapping.ApplyAction(entity, @event);
                }
            }

            return entity;
        }
    }
}