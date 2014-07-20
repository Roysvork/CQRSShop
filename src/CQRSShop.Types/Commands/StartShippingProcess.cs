namespace CQRSShop.Types.Commands
{
    using System;

    public class StartShippingProcess
    {
        public StartShippingProcess(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }
    }
}