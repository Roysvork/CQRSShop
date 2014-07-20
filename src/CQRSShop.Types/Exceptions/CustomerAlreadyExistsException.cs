namespace CQRSShop.Types.Exceptions
{
    using System;

    public class CustomerAlreadyExistsException : DuplicateAggregateException
    {
        public CustomerAlreadyExistsException(Guid id) : base(id)
        {
        }
    }
}