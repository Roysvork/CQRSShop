using CQRSShop.Types;
using Simple.Web;

namespace CQRSShop.Web.Api.Basket.Pay
{
    using CQRSShop.Types.Commands;

    [UriTemplate("/api/basket/{BasketId}/pay")]
    public class PostEndpoint : BasePostEndpoint<MakePayment>
    {
         
    }
}