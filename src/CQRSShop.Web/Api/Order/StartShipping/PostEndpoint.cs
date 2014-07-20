using CQRSShop.Types;
using Simple.Web;

namespace CQRSShop.Web.Api.Order.StartShipping
{
    using CQRSShop.Types.Commands;

    [UriTemplate("/api/order/{OrderId}/startshipping")]
    public class PostEndpoint : BasePostEndpoint<StartShippingProcess>
    {
         
    }
}