using CQRSShop.Types;
using Simple.Web;

namespace CQRSShop.Web.Api.Order.Approve
{
    using CQRSShop.Types.Commands;

    [UriTemplate("/api/order/{OrderId}/approve")]
    public class PostEndpoint : BasePostEndpoint<ApproveOrder>
    {
         
    }
}