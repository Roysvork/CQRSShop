namespace CQRSShop.Types.Commands
{
    using System;

    public class CreateBasket
    {
        public CreateBasket(Guid id, Guid customerId)
        {
            this.Id = id;
            this.CustomerId = customerId;
        }

        public Guid Id { get; private set; }
        
        public Guid CustomerId { get; private set; }
    }
}