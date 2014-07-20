namespace CQRSShop.Web.Api
{
    using System;

    using CQRSShop.Application;
    using CQRSShop.Infrastructure;

    using Simple.Web;
    using Simple.Web.Behaviors;

    public abstract class BasePostEndpoint<TCommand> : IPost, IInput<TCommand>
    {
        public TCommand Input { private get; set; }

        public Status Post()
        {
            try
            {
                CQRS.Shop.CommandDispatcher.Dispatch(this.Input);
            }
            catch (Exception)
            {
                return Status.InternalServerError;
            }

            return Status.OK;
        }
    }
}