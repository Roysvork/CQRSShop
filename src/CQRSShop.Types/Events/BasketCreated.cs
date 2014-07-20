namespace CQRSShop.Types.Events
{
    using System;

    public class BasketCreated
    {
        public BasketCreated(Guid id, Guid customerId, int discount)
        {
            this.Id = id;
            this.CustomerId = customerId;
            this.Discount = discount;
        }

        public Guid Id { get; private set; }

        public Guid CustomerId { get; private set; } 
        
        public int Discount { get; private set; }

        protected bool Equals(BasketCreated other)
        {
            return this.Id.Equals(other.Id) && this.CustomerId.Equals(other.CustomerId) && this.Discount == other.Discount;
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
            return this.Equals((BasketCreated)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ this.CustomerId.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Discount;
                return hashCode;
            }
        }
    }
}