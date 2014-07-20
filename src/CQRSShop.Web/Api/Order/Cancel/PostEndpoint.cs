using CQRSShop.Types;
using Simple.Web;

namespace CQRSShop.Web.Api.Order.Cancel
{
    using CQRSShop.Types.Commands;

    [UriTemplate("/api/order/{OrderId}/cancel")]
    public class PostEndpoint : BasePostEndpoint<CancelOrder>
    {
         
    }
}