using CQRSShop.Types;
using Simple.Web;

namespace CQRSShop.Web.Api.Basket.Proceed
{
    using CQRSShop.Types.Commands;

    [UriTemplate("/api/basket/{BasketId}/proceed")]
    public class PostEndpoint : BasePostEndpoint<ProceedToCheckout>
    {
         
    }
}