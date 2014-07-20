namespace CQRSShop.Types.Commands
{
    using System;

    public class CreateProduct
    {
        public CreateProduct(Guid id, string name, int price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public Guid Id { get; private set; }
        
        public string Name { get; private set; }

        public int Price { get; private set; }
    }
}