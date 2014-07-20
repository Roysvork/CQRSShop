namespace CQRSShop.Types.Events
{
    using System;

    // Customer events
    public class CustomerCreated
    {
        public CustomerCreated(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        protected bool Equals(CustomerCreated other)
        {
            return this.Id.Equals(other.Id) && string.Equals(this.Name, other.Name);
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
            return this.Equals((CustomerCreated)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Id.GetHashCode() * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
            }
        }
    }

    // Product events

    // Basket events

    // Order events
}
