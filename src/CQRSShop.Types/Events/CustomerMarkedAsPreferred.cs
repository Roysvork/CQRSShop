namespace CQRSShop.Types.Events
{
    using System;

    public class CustomerMarkedAsPreferred
    {
        public CustomerMarkedAsPreferred(Guid id, int discount)
        {
            this.Id = id;
            this.Discount = discount;
        }

        public Guid Id { get; private set; }

        public int Discount { get; private set; }

        protected bool Equals(CustomerMarkedAsPreferred other)
        {
            return this.Id.Equals(other.Id) && this.Discount == other.Discount;
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
            return this.Equals((CustomerMarkedAsPreferred)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Id.GetHashCode() * 397) ^ this.Discount;
            }
        }
    }
}