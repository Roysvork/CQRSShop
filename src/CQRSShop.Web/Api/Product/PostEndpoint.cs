using CQRSShop.Types;
using Simple.Web;
using Simple.Web.Links;

namespace CQRSShop.Web.Api.Product
{
    using CQRSShop.Types.Commands;

    [UriTemplate("/api/product")]
    [Root(Rel = "product", Title = "Product", Type = "application/vnd.cqrsshop.createproduct")]
    public class PostEndpoint : BasePostEndpoint<CreateProduct>
    {
         
    }
}