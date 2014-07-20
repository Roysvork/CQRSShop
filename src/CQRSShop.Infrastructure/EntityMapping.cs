namespace CQRSShop.Infrastructure
{
    using System;

    public class EntityMapping
    {
        public Type EntityType { get; set; }

        public Action<object, object> ApplyAction { get; set; }
    }
}
