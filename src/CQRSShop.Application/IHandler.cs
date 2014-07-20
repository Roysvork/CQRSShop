namespace CQRSShop.Application
{
    public interface IHandler<in T>
    {
        void Handle(T command);
    }
}
