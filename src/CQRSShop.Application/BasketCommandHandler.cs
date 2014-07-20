namespace CQRSShop.Application
{
    using CQRSShop.Domain;
    using CQRSShop.Infrastructure;
    using CQRSShop.Infrastructure.Exceptions;
    using CQRSShop.Types;
    using CQRSShop.Types.Commands;
    using CQRSShop.Types.Exceptions;

    public class BasketCommandHandler :
        IHandler<CreateBasket>, 
        IHandler<AddItemToBasket>, 
        IHandler<ProceedToCheckout>, 
        IHandler<CheckoutBasket>, 
        IHandler<MakePayment>
    {
        private readonly IRepository<Basket> basketRepository;

        private readonly IRepository<Product> productRepository;

        private readonly IRepository<Customer> customerRepository;

        public BasketCommandHandler(
            IRepository<Basket> basketRepository,
            IRepository<Product> productRepository,
            IRepository<Customer> customerRepository)
        {
            this.basketRepository = basketRepository;
            this.productRepository = productRepository;
            this.customerRepository = customerRepository;
        }

        public void Handle(CreateBasket command)
        {
            try
            {
                var basket = this.basketRepository.GetById(command.Id);
                throw new BasketAlreadExistsException(command.Id);
            }
            catch (EntityNotFoundException)
            {
                // Expect this
            }
            
            var customer = this.customerRepository.GetById(command.CustomerId);
            Basket.Create(command.Id, customer);
        }

        public void Handle(AddItemToBasket command)
        {
            var basket = this.basketRepository.GetById(command.Id);
            var product = this.productRepository.GetById(command.ProductId);

            basket.AddItem(product, command.Quantity);
        }

        public void Handle(ProceedToCheckout command)
        {
            var basket = this.basketRepository.GetById(command.Id);
            basket.ProceedToCheckout();
        }

        public void Handle(CheckoutBasket command)
        {
            var basket = this.basketRepository.GetById(command.Id);
            basket.Checkout(command.ShippingAddress);
        }

        public void Handle(MakePayment command)
        {
            var basket = this.basketRepository.GetById(command.Id);
            basket.MakePayment(command.Payment);
        }
    }
}