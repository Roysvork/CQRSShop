namespace CQRSShop.Types.Events
{
    using System;

    public class ProductCreated
    {
        public ProductCreated(Guid id, string name, int price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; } 
        
        public int Price { get; private set; }

        protected bool Equals(ProductCreated other)
        {
            return this.Id.Equals(other.Id) && string.Equals(this.Name, other.Name) && this.Price == other.Price;
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
            return this.Equals((ProductCreated)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.Price;
                return hashCode;
            }
        }
    }
}