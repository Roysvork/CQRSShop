namespace CQRSShop.Application
{
    using System;
    using System.Collections.Generic;

    using CQRSShop.Domain;
    using CQRSShop.Infrastructure;
    using CQRSShop.Types.Commands;
    using CQRSShop.Types.Events;

    public class Shop
    {
        public Shop()
        {
            var entityMappings = CreateEntityEventMappings();

            this.EventBus = new InMemoryEventBus(CreateKeyMappings(), entityMappings);
            this.CommandDispatcher = CreateCommandDispatcher(this.EventBus, entityMappings);

            Event.RaiseAction = o => this.EventBus.Raise(o);
        }

        public ICommandDispatcher CommandDispatcher { get; private set; }

        public IEventBus EventBus { get; private set; }

        private static Dictionary<Type, EntityMapping> CreateEntityEventMappings()
        {
            var entityMappings = new Dictionary<Type, EntityMapping>();
            entityMappings.AddMapping<Customer, CustomerCreated>((o, e) => o.Apply(e));
            entityMappings.AddMapping<Customer, CustomerMarkedAsPreferred>((o, e) => o.Apply(e));
 
            entityMappings.AddMapping<Product, ProductCreated>((o, e) => o.Apply(e));
            
            entityMappings.AddMapping<Basket, BasketCreated>((o, e) => o.Apply(e));
            entityMappings.AddMapping<Basket, ItemAdded>((o, e) => o.Apply(e));
            entityMappings.AddMapping<Basket, CustomerIsCheckingOutBasket>();
            entityMappings.AddMapping<Basket, BasketCheckedOut>();

            entityMappings.AddMapping<Order, OrderCreated>((o, e) => o.Apply(e));
            entityMappings.AddMapping<Order, ShippingProcessStarted>((o, e) => o.Apply(e));
            entityMappings.AddMapping<Order, OrderCancelled>((o, e) => o.Apply(e));

            entityMappings.AddMapping<Order, OrderShipped>();
            entityMappings.AddMapping<Order, NeedsApproval>();
            entityMappings.AddMapping<Order, OrderApproved>();
            
            return entityMappings;
        } 

        private static Dictionary<Type, Func<object, object>> CreateKeyMappings()
        {
            var keyMappings = new Dictionary<Type, Func<object, object>>();
            keyMappings.AddMapping<CustomerCreated>(o => o.Id);
            keyMappings.AddMapping<CustomerMarkedAsPreferred>(o => o.Id);
            keyMappings.AddMapping<ProductCreated>(o => o.Id);
            keyMappings.AddMapping<BasketCreated>(o => o.Id);
            keyMappings.AddMapping<ItemAdded>(o => o.Id);
            keyMappings.AddMapping<CustomerIsCheckingOutBasket>(o => o.Id);
            keyMappings.AddMapping<BasketCheckedOut>(o => o.Id);
            keyMappings.AddMapping<OrderCreated>(o => o.Id);
            keyMappings.AddMapping<ShippingProcessStarted>(o => o.Id);
            keyMappings.AddMapping<OrderCancelled>(o => o.Id);
            keyMappings.AddMapping<OrderShipped>(o => o.Id);
            keyMappings.AddMapping<NeedsApproval>(o => o.Id);
            keyMappings.AddMapping<OrderApproved>(o => o.Id);

            return keyMappings;
        }

        private static InMemoryCommandDispatcher CreateCommandDispatcher(IEventBus eventBus, Dictionary<Type, EntityMapping> applyMappings)
        {
            var dispatcher = new InMemoryCommandDispatcher();

            var customerRepository = new Repository<Customer>(eventBus, applyMappings);
            var customerCommandHandler = new CustomerCommandHandler(customerRepository);
            dispatcher.RegisterHandler<CreateCustomer>(customerCommandHandler.Handle);
            dispatcher.RegisterHandler<MarkCustomerAsPreferred>(customerCommandHandler.Handle);

            var productRepository = new Repository<Product>(eventBus, applyMappings);
            var productCommandHandler = new ProductCommandHandler(productRepository);
            dispatcher.RegisterHandler<CreateProduct>(productCommandHandler.Handle);

            var basketCommandHandler = new BasketCommandHandler(
                new Repository<Basket>(eventBus, applyMappings),
                productRepository,
                customerRepository);

            dispatcher.RegisterHandler<CreateBasket>(basketCommandHandler.Handle);
            dispatcher.RegisterHandler<AddItemToBasket>(basketCommandHandler.Handle);
            dispatcher.RegisterHandler<ProceedToCheckout>(basketCommandHandler.Handle);
            dispatcher.RegisterHandler<CheckoutBasket>(basketCommandHandler.Handle);
            dispatcher.RegisterHandler<MakePayment>(basketCommandHandler.Handle);

            var orderCommandHandler = new OrderCommandHandler(new Repository<Order>(eventBus, applyMappings));
            dispatcher.RegisterHandler<ApproveOrder>(orderCommandHandler.Handle);
            dispatcher.RegisterHandler<StartShippingProcess>(orderCommandHandler.Handle);
            dispatcher.RegisterHandler<CancelOrder>(orderCommandHandler.Handle);
            dispatcher.RegisterHandler<ShipOrder>(orderCommandHandler.Handle);

            return dispatcher;
        }
    }
}
