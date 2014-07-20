namespace CQRSShop.Application
{
    using CQRSShop.Domain;
    using CQRSShop.Infrastructure;
    using CQRSShop.Types;
    using CQRSShop.Types.Commands;

    public class OrderCommandHandler : 
        IHandler<ApproveOrder>, 
        IHandler<StartShippingProcess>, 
        IHandler<CancelOrder>, 
        IHandler<ShipOrder>
    {
        private readonly IRepository<Order> orderRepository;

        public OrderCommandHandler(IRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void Handle(ApproveOrder command)
        {
            var order = this.orderRepository.GetById(command.Id);
            order.Approve();
        }

        public void Handle(StartShippingProcess command)
        {
            var order = this.orderRepository.GetById(command.Id);
            order.StartShippingProcess();
        }

        public void Handle(CancelOrder command)
        {
            var order = this.orderRepository.GetById(command.Id);
            order.Cancel();
        }

        public void Handle(ShipOrder command)
        {
            var order = this.orderRepository.GetById(command.Id);
            order.ShipOrder();
        }
    }
}