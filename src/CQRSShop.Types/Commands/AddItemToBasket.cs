namespace CQRSShop.Types.Commands
{
    using System;

    public class AddItemToBasket
    {
        public AddItemToBasket(Guid id, Guid productId, int quantity)
        {
            this.Id = id;
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        public Guid Id { get; private set; }
    
        public Guid ProductId { get; private set; }
        
        public int Quantity { get; private set; }
    }
}