namespace CQRSShop.Domain
{
    using System;

    using CQRSShop.Types.Events;

    public class Product
    {
        private Product(Guid id, string name, int price)
        {
            Event.Raise(new ProductCreated(id, name, price));
        }

        public string Name { get; private set; }

        public int Price { get; private set; }

        public Guid Id { get; set; }

        public static Product Create(Guid id, string name, int price)
        {
            return new Product(id, name, price);
        }

        public void Apply(ProductCreated obj)
        {
            this.Id = obj.Id;
            this.Name = obj.Name;
            this.Price = obj.Price;
        }
    }
}