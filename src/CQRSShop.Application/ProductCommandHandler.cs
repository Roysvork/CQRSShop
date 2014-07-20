namespace CQRSShop.Application
{
    using CQRSShop.Domain;
    using CQRSShop.Infrastructure;
    using CQRSShop.Infrastructure.Exceptions;
    using CQRSShop.Types;
    using CQRSShop.Types.Commands;
    using CQRSShop.Types.Exceptions;

    public class ProductCommandHandler : 
        IHandler<CreateProduct>
    {
        private readonly IRepository<Product> productRepository;

        public ProductCommandHandler(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public void Handle(CreateProduct command)
        {
            try
            {
                var product = this.productRepository.GetById(command.Id);
                throw new ProductAlreadyExistsException(command.Id);
            }
            catch (EntityNotFoundException)
            {
                // We expect not to find anything
            }

            Product.Create(command.Id, command.Name, command.Price);
        }
    }
}