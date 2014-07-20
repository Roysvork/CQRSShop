using CQRSShop.Types;
using Simple.Web;
using Simple.Web.Links;

namespace CQRSShop.Web.Api.Customer
{
    using CQRSShop.Types.Commands;

    [UriTemplate("/api/customer")]
    [Root(Rel = "order", Title = "Order", Type = "application/vnd.cqrsshop.createcustomer")]
    public class PostEndpoint : BasePostEndpoint<CreateCustomer>
    {
         
    }
}