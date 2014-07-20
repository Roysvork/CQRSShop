namespace CQRSShop.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CQRSShop.Types;
    using CQRSShop.Types.Events;
    using CQRSShop.Types.Exceptions;

    public class Basket
    {
        private int baseDiscount;

        private List<OrderLine> orderLines;

        private Basket(Guid id, Guid customerId, int discount)
        {  
            Event.Raise(new BasketCreated(id, customerId, discount));
        }

        public Guid Id { get; set; }

        public static Basket Create(Guid id, Customer customer)
        {
            return new Basket(id, customer.Id, customer.Discount);
        }

        public void Apply(ItemAdded obj)
        {
            this.orderLines.Add(obj.OrderLine);
        }

        public void Apply(BasketCreated obj)
        {
            this.Id = obj.Id;
            this.baseDiscount = obj.Discount;
            this.orderLines = new List<OrderLine>();
        }
        
        public void AddItem(Product product, int quantity)
        {
            var discount = (int)(product.Price * ((double)this.baseDiscount / 100));
            var discountedPrice = product.Price - discount;

            var orderLine = new OrderLine(product.Id, product.Name, product.Price, discountedPrice, quantity);
            Event.Raise(new ItemAdded(this.Id, orderLine));
        }

        public void ProceedToCheckout()
        {
            Event.Raise(new CustomerIsCheckingOutBasket(this.Id));
        }

        public void Checkout(Address shippingAddress)
        {
            if (shippingAddress == null || string.IsNullOrWhiteSpace(shippingAddress.Street))
            {
                throw new MissingAddressException();
            }

            Event.Raise(new BasketCheckedOut(this.Id, shippingAddress));
        }

        public void MakePayment(int payment)
        {
            var expectedPayment = this.orderLines.Sum(y => y.DiscountedPrice * y.Quantity);
            if (expectedPayment != payment)
            {
                throw new UnexpectedPaymentException();
            }

            Order.Create(this.Id, this.orderLines);
        }
    }
}