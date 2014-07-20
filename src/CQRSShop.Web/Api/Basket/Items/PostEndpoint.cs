namespace CQRSShop.Web.Api.Basket.Items
{
    using CQRSShop.Types;
    using CQRSShop.Types.Commands;

    using Simple.Web;

    [UriTemplate("/api/basket/{BasketId}/items")]
    public class PostEndpoint : BasePostEndpoint<AddItemToBasket>
    {
         
    }
}