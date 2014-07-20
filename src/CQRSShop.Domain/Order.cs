namespace CQRSShop.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CQRSShop.Types;
    using CQRSShop.Types.Events;
    using CQRSShop.Types.Exceptions;

    internal enum OrderState
    {
        ShippingProcessStarted,
        Created,
        Cancelled
    }

    public class Order
    {
        private OrderState orderState;

        public Guid Id { get; set; }

        public static Order Create(Guid basketId, List<OrderLine> orderLines)
        {
            var id = IdGenerator.GenerateGuid();

            Event.Raise(new OrderCreated(id, basketId, orderLines));

            var totalPrice = orderLines.Sum(y => y.DiscountedPrice);
            if (totalPrice > 100000)
            {
                Event.Raise(new NeedsApproval(id));
            }
            else
            {
                Event.Raise(new OrderApproved(id));
            }

            return new Order();
        }

        public void Apply(OrderCancelled obj)
        {
            this.orderState = OrderState.Cancelled;
        }

        public void Apply(ShippingProcessStarted obj)
        {
            this.orderState = OrderState.ShippingProcessStarted;
        }

        public void Apply(OrderCreated obj)
        {
            this.Id = obj.Id;
            this.orderState = OrderState.Created;
        }

        public void Approve()
        {
            Event.Raise(new OrderApproved(this.Id));
        }

        public void StartShippingProcess()
        {
            if (this.orderState == OrderState.Cancelled)
            {
                throw new OrderCancelledException();
            }

            Event.Raise(new ShippingProcessStarted(this.Id));
        }

        public void Cancel()
        {
            if (this.orderState == OrderState.Created)
            {
                Event.Raise(new OrderCancelled(this.Id));
            }
            else
            {
                throw new ShippingStartedException();
            }
        }

        public void ShipOrder()
        {
            if (this.orderState != OrderState.ShippingProcessStarted)
            {
                throw new InvalidOrderState();
            }
                
            Event.Raise(new OrderShipped(this.Id));
        }
    }
}