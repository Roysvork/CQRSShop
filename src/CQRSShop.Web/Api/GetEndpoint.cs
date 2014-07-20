namespace CQRSShop.Web.Api
{
    using System.Collections.Generic;

    using Simple.Web;
    using Simple.Web.Behaviors;
    using Simple.Web.Links;

    [UriTemplate("/api")]
    public class GetEndpoint : IGet, IOutput<IEnumerable<Link>>
    {
        public IEnumerable<Link> Output { get; set; }

        public Status Get()
        {
            this.Output = LinkHelper.GetRootLinks();
            return 200;
        }
    }
}