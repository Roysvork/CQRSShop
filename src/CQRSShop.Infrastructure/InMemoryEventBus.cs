namespace CQRSShop.Infrastructure
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class InMemoryEventBus : IEventBus
    {
        private readonly Dictionary<Type, Func<object, object>> keyMappings;

        private readonly Dictionary<Type, EntityMapping> entityMappings;

        private readonly ConcurrentQueue<object> committedEvents;

        private readonly ConcurrentDictionary<object, ConcurrentQueue<object>> pendingEventContainers;

        public InMemoryEventBus(
            Dictionary<Type, Func<object, object>> keyMappings,
            Dictionary<Type, EntityMapping> entityMappings)
        {
            this.keyMappings = keyMappings;
            this.entityMappings = entityMappings;
            this.committedEvents = new ConcurrentQueue<object>();
            this.pendingEventContainers = new ConcurrentDictionary<object, ConcurrentQueue<object>>();
        }

        public IEnumerable<object> Get<TEntity>(object eventKey)
        {
            var relevantEventTypes = this.GetReleventEventTypesFor<TEntity>();

            return
                this.committedEvents.Where(
                    o => relevantEventTypes.Contains(o.GetType()) && this.GetKeyFor(o).Equals(eventKey));
        }

        public IEnumerable<object> GetAll()
        {
            return this.committedEvents;
        }

        public IEnumerable<object> GetAllUncommitted()
        {
            return this.pendingEventContainers.Values.SelectMany(o => o);
        }

        public void Raise(object domainEvent)
        {
            var eventType = domainEvent.GetType();
            if (!this.entityMappings.ContainsKey(eventType))
            {
                throw new InvalidOperationException(string.Format("No entity mapping exists for event {0}", eventType));
            }

            var entityType = this.entityMappings[eventType].EntityType;
            var key = BuildEventKey(entityType, this.GetKeyFor(domainEvent));
            if (!this.pendingEventContainers.ContainsKey(key))
            {
                this.pendingEventContainers.TryAdd(key, new ConcurrentQueue<object>());
            }

            var container = this.pendingEventContainers[key];
            container.Enqueue(domainEvent);
        }

        public void Commit<TEntity>(object eventKey)
        {
            var key = BuildEventKey(typeof(TEntity), eventKey);
            if (!this.pendingEventContainers.ContainsKey(key))
            {
                return;
            }

            var container = this.pendingEventContainers[key];

            object domainEvent;
            while (container.TryDequeue(out domainEvent))
            {
                this.committedEvents.Enqueue(domainEvent);
            }
        }

        public void CommitAll()
        {
            foreach (var container in this.pendingEventContainers.Values)
            {
                object domainEvent;
                while (container.TryDequeue(out domainEvent))
                {
                    this.committedEvents.Enqueue(domainEvent);
                }
            }
        }

        public void Publish()
        {
            // Publish is not relevant for in memory event bus
        }

        private static string BuildEventKey(Type type, object key)
        {
            return string.Format("{0}_{1}", type, key);
        }

        private object GetKeyFor(object domainEvent)
        {
            var type = domainEvent.GetType();
            if (!this.keyMappings.ContainsKey(type))
            {
                throw new InvalidOperationException(
                    string.Format("Key mapping for event {0} was not found", type));
            }

            var mapping = this.keyMappings[type];
            return mapping(domainEvent);
        }

        private IEnumerable<Type> GetReleventEventTypesFor<TEntity>()
        {
            return this.entityMappings.Where(o => o.Value.EntityType == typeof(TEntity)).Select(o => o.Key);
        }
    }
}
