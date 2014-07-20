namespace CQRSShop.Types
{
    using System;

    public class OrderLine
    {
        public OrderLine(Guid id, string productName, int originalPrice, int discountedPrice, int quantity)
        {
            this.Id = id;
            this.ProductName = productName;
            this.OriginalPrice = originalPrice;
            this.DiscountedPrice = discountedPrice;
            this.Quantity = quantity;
        }

        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public int OriginalPrice { get; set; }

        public int DiscountedPrice { get; set; }

        public int Quantity { get; set; }

        public override string ToString()
        {
            return string.Format(
                "ProdcutName: {0}, Price: {1}, Discounted: {2}, Quantity: {3}",
                this.ProductName,
                this.OriginalPrice,
                this.DiscountedPrice,
                this.Quantity);
        }

        protected bool Equals(OrderLine other)
        {
            return this.Id.Equals(other.Id) && string.Equals(this.ProductName, other.ProductName) && this.OriginalPrice == other.OriginalPrice && this.DiscountedPrice == other.DiscountedPrice && this.Quantity == other.Quantity;
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
            return this.Equals((OrderLine)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.ProductName != null ? this.ProductName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.OriginalPrice;
                hashCode = (hashCode * 397) ^ this.DiscountedPrice;
                hashCode = (hashCode * 397) ^ this.Quantity;
                return hashCode;
            }
        }
    }
}