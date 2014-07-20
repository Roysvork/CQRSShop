namespace CQRSShop.Types.Commands
{
    using System;

    public class ApproveOrder
    {
        public ApproveOrder(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }
    }
}