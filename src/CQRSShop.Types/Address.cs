namespace CQRSShop.Types
{
    public class Address
    {
        public Address(string street)
        {
            this.Street = street;
        }

        public string Street { get; set; }

        protected bool Equals(Address other)
        {
            return string.Equals(this.Street, other.Street);
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
            return Equals((Address)obj);
        }

        public override int GetHashCode()
        {
            return (this.Street != null ? this.Street.GetHashCode() : 0);
        }
    }
}
