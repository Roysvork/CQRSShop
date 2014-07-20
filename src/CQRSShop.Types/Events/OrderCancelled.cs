namespace CQRSShop.Types.Events
{
    using System;

    public class OrderCancelled
    {
        public OrderCancelled(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }

        protected bool Equals(OrderCancelled other)
        {
            return this.Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return this.Equals((OrderCancelled)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}