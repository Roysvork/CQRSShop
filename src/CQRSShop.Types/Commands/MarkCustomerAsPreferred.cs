namespace CQRSShop.Types.Commands
{
    using System;

    public class MarkCustomerAsPreferred
    {
        public MarkCustomerAsPreferred(Guid id, int discount)
        {
            this.Id = id;
            this.Discount = discount;
        }

        public Guid Id { get; private set; }

        public int Discount { get; private set; }
    }
}