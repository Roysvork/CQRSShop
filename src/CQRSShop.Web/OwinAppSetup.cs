using CQRSShop.Web;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartup(typeof(OwinAppSetup))]

namespace CQRSShop.Web
{
    public class OwinAppSetup
    {
        public void Configuration(IAppBuilder app)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            app.Run(
                context => Simple.Web.Application.App(
                    _ =>
                        {
                            var task = context.Response.WriteAsync("Hello world!");
                            return task;
                        })(context.Environment));
        }
    }
}
