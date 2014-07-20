namespace CQRSShop.Types.Events
{
    using System;

    public class BasketCheckedOut
    {
        public BasketCheckedOut(Guid id, Address shippingAddress)
        {
            this.Id = id;
            this.ShippingAddress = shippingAddress;
        }

        public Guid Id { get; private set; }

        public Address ShippingAddress { get; private set; }

        protected bool Equals(BasketCheckedOut other)
        {
            return this.Id.Equals(other.Id) && Equals(this.ShippingAddress, other.ShippingAddress);
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
            return this.Equals((BasketCheckedOut)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Id.GetHashCode() * 397) ^ (this.ShippingAddress != null ? this.ShippingAddress.GetHashCode() : 0);
            }
        }
    }
}