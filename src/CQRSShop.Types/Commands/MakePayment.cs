namespace CQRSShop.Types.Commands
{
    using System;

    public class MakePayment
    {
        public MakePayment(Guid id, int payment)
        {
            this.Id = id;
            this.Payment = payment;
        }

        public Guid Id { get; private set; }
        
        public int Payment { get; private set; }
    }
}