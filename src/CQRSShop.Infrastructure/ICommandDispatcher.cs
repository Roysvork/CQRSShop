namespace CQRSShop.Infrastructure
{
    public interface ICommandDispatcher
    {
        void Dispatch(object command);
    }
}