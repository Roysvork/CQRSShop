namespace CQRSShop.Types.Commands
{
    using System;

    public class ShipOrder
    {
        public ShipOrder(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }
    }
}