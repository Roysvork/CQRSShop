namespace CQRSShop.Types.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderCreated
    {
        public OrderCreated(Guid id, Guid basketId, List<OrderLine> orderLines)
        {
            this.Id = id;
            this.BasketId = basketId;
            this.OrderLines = orderLines;
        }

        public Guid Id { get; private set; }

        public Guid BasketId { get; private set; }

        public List<OrderLine> OrderLines { get; private set; }

        protected bool Equals(OrderCreated other)
        {
            return this.Id.Equals(other.Id) && this.BasketId.Equals(other.BasketId) && this.OrderLines.SequenceEqual(other.OrderLines);
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
            return this.Equals((OrderCreated)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ this.BasketId.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.OrderLines != null ? this.OrderLines.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}