using SmartList.Contracts;

namespace smartList.Routes
{
    public static class ShopListRoutes
    {
        public static void MapShopListRoutes(this IEndpointRouteBuilder app)
        {

            var shopListGroup = app.MapGroup("api/shopList")
                .WithTags("ShopList");

            shopListGroup.MapPost("create", createShopList)
                .WithName(nameof(createShopList))
            .Produces<bool>(200);

            shopListGroup.MapGet("satistic/{userId}", getShopListSatistic)
                .WithName(nameof(getShopListSatistic))
            .Produces<ShopListDto>(200);


        }

        public static async Task<Ok<ShopListDto>> getShopListSatistic(int userId, IShopListService shopListService)
        {
            var result = await shopListService.GetShopListSatistic(userId);
            return TypedResults.Ok(result);
        }
        public static async Task<Ok<bool>> createShopList(ShopListDto productDetailsInShopDto, IShopListService shopListService)
        {
            var result = await shopListService.CreateListShop(productDetailsInShopDto);
            return TypedResults.Ok(result);
        }


    }
}
