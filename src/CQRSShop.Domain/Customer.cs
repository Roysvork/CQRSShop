namespace CQRSShop.Domain
{
    using System;

    using CQRSShop.Types.Events;

    public class Customer
    {
        private Customer(Guid id, string name)
        {
            Event.Raise(new CustomerCreated(id, name));
        }

        public Guid Id { get; set; }

        public int Discount { get; set; }

        public static Customer Create(Guid id, string name)
        {
            return new Customer(id, name);
        }

        public void Apply(CustomerCreated obj)
        {
            this.Id = obj.Id;
        }

        public void Apply(CustomerMarkedAsPreferred obj)
        {
            this.Discount = obj.Discount;
        }
        
        public void MakePreferred(int discount)
        {
            Event.Raise(new CustomerMarkedAsPreferred(this.Id, discount));
        }
    }
}