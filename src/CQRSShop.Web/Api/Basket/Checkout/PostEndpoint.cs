namespace CQRSShop.Web.Api.Basket.Checkout
{
    using CQRSShop.Types;
    using CQRSShop.Types.Commands;

    using Simple.Web;

    [UriTemplate("/api/basket/{BasketId}/checkout")]
    public class PostEndpoint : BasePostEndpoint<AddItemToBasket>
    {
         
    }
}