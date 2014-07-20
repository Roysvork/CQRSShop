namespace CQRSShop.Types.Events
{
    using System;

    public class ItemAdded
    {
        public ItemAdded(Guid id, OrderLine orderLine)
        {
            this.Id = id;
            this.OrderLine = orderLine;
        }

        public Guid Id { get; private set; }

        public OrderLine OrderLine { get; private set; }

        public override string ToString()
        {
            return string.Format("Item added. Id: {0},{1}", this.Id, this.OrderLine);
        }

        protected bool Equals(ItemAdded other)
        {
            return this.Id.Equals(other.Id) && Equals(this.OrderLine, other.OrderLine);
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
            return this.Equals((ItemAdded)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Id.GetHashCode() * 397) ^ (this.OrderLine != null ? this.OrderLine.GetHashCode() : 0);
            }
        }
    }
}