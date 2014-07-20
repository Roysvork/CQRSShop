namespace CQRSShop.Domain
{
    using System;

    public class IdGenerator
    {
        private static Func<Guid> generator;

        public static Func<Guid> GenerateGuid
        {
            get
            {
                generator = generator ?? Guid.NewGuid;
                return generator;
            }

            set
            {
                generator = value;
            }
        }
    }
}