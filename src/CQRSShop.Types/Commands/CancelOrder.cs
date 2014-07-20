namespace CQRSShop.Types.Commands
{
    using System;

    public class CancelOrder
    {
        public CancelOrder(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }
    }
}