namespace CQRSShop.Types.Commands
{
    using System;

    public class ProceedToCheckout
    {
        public ProceedToCheckout(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }
    }
}