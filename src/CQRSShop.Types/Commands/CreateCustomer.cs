namespace CQRSShop.Types.Commands
{
    using System;

    // Customer commands
    public class CreateCustomer
    {
        public CreateCustomer(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }

    // Product commands

    // Basket commands

    // Order commands
}
