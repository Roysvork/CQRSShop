using CQRSShop.Types;
using Simple.Web;

namespace CQRSShop.Web.Api.Order.Ship
{
    using CQRSShop.Types.Commands;

    [UriTemplate("/api/order/{OrderId}/ship")]
    public class PostEndpoint : BasePostEndpoint<ShipOrder>
    {
         
    }
}