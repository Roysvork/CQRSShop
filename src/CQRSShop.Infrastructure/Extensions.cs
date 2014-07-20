namespace CQRSShop.Infrastructure
{
    using System;
    using System.Collections.Generic;

    public static class Extensions
    {
        public static void AddMapping<T>(this Dictionary<Type, Func<object, object>> mappings, Func<T, object> newMapping)
        {
            mappings.Add(typeof(T), o => newMapping((T)o));
        }

        public static void AddMapping<TEntity, TEvent>(
            this Dictionary<Type, EntityMapping> mappings,
            Action<TEntity, TEvent> applyAction)
        {
            var applyMapping = new EntityMapping
                                   {
                                       EntityType = typeof(TEntity),
                                       ApplyAction = (entity, e) => applyAction((TEntity)entity, (TEvent)e)
                                   };

            mappings.Add(typeof(TEvent), applyMapping);
        }

        public static void AddMapping<TEntity, TEvent>(
            this Dictionary<Type, EntityMapping> mappings)
        {
            AddMapping<TEntity, TEvent>(mappings, (entity, @event) => { });
        }
    }
}
