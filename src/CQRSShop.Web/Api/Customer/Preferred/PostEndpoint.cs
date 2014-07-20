using CQRSShop.Types;
using Simple.Web;

namespace CQRSShop.Web.Api.Customer.Preferred
{
    using CQRSShop.Types.Commands;

    [UriTemplate("/api/customer/{CustomerId}/preferred")]
    public class PostEndpoint : BasePostEndpoint<MarkCustomerAsPreferred>
    {
         
    }
}