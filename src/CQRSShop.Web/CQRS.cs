namespace CQRSShop.Web
{
    using CQRSShop.Application;

    public static class CQRS
    {
        static CQRS()
        {
            Shop = new Shop();
        }

        public static Shop Shop { get; private set; }
    }
}