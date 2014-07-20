namespace CQRSShop.Web
{
    using System.Collections.Generic;

    using CQRSShop.Infrastructure;

    using Ninject.Modules;

    public class DependencySetup : Simple.Web.Ninject.NinjectStartupBase
    {
        protected override IEnumerable<INinjectModule> CreateModules()
        {
            yield return new Module();
        }

        public class Module : NinjectModule
        {
            public override void Load()
            {
                
            }
        }
    }
}