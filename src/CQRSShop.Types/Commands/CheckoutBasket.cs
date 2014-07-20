namespace CQRSShop.Types.Commands
{
    using System;

    public class CheckoutBasket
    {
        public CheckoutBasket(Guid id, Address shippingAddress)
        {
            this.Id = id;
            this.ShippingAddress = shippingAddress;
        }

        public Guid Id { get; private set; }
    
        public Address ShippingAddress { get; private set; }
    }
}