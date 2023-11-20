using Microsoft.AspNetCore.Http.HttpResults;
using SmartList.Contracts;

namespace smartList.Routes
{
    public static class ProductsRoutes
    {
        public static void MapProductsRoutes(this IEndpointRouteBuilder app)
        {

            var productGroup = app.MapGroup("api/products")
                .WithTags("Products");

            productGroup.MapGet("getList", GetProductList)
                .WithName(nameof(GetProductList))
                .Produces<List<ProductDto>>(200);

            productGroup.MapPost("add", AddProduct)
               .WithName(nameof(AddProduct))
               .Produces<ProductDto>(200);

        }
        public static async Task<Ok<List<ProductDto>>> GetProductList(IProductsServiceService productsServiceService)
        {
            var result = await productsServiceService.GetProductList();
            return TypedResults.Ok(result);
        }

        public static async Task<Ok<ProductDto>> AddProduct(ProductDto productDto, IProductsServiceService productsServiceService)
        {
            var result = await productsServiceService.AddProduct(productDto);
            return TypedResults.Ok(result);
        }
      
    }
}
