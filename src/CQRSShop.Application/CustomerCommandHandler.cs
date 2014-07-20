namespace CQRSShop.Application
{
    using CQRSShop.Domain;
    using CQRSShop.Infrastructure;
    using CQRSShop.Infrastructure.Exceptions;
    using CQRSShop.Types;
    using CQRSShop.Types.Commands;
    using CQRSShop.Types.Exceptions;

    public class CustomerCommandHandler : 
        IHandler<CreateCustomer>, 
        IHandler<MarkCustomerAsPreferred>
    {
        private readonly IRepository<Customer> customerRepository;

        public CustomerCommandHandler(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void Handle(CreateCustomer command)
        {
            try
            {
                var customer = this.customerRepository.GetById(command.Id);
                throw new CustomerAlreadyExistsException(command.Id);
            }
            catch (EntityNotFoundException)
            {
                // We expect not to find anything
            }

            Customer.Create(command.Id, command.Name);
        }

        public void Handle(MarkCustomerAsPreferred command)
        {
            var customer = this.customerRepository.GetById(command.Id);
            customer.MakePreferred(command.Discount);
        }
    }
}