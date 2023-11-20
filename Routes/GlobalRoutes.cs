namespace smartList.Routes
{
    public static class GlobalRoutes
    {
        public static void MapGlobalRoutes(this IEndpointRouteBuilder app)
        {

            var globalGroup = app.MapGroup("api/global")
                .WithTags("Global");

            globalGroup.MapGet("getComanyList", getComanyList)
                .WithName(nameof(getComanyList))
            .Produces<List<MetadataDto>>(200);

            globalGroup.MapGet("getCategoryList", getCategoryList)
            .WithName(nameof(getCategoryList))
            .Produces<List<MetadataDto>>(200);

        }
        public static async Task<Ok<List<MetadataDto>>> getComanyList(IGlobalServiceService globalServiceService)
        {
            var result = await globalServiceService.GetCompanyList();
            return TypedResults.Ok(result);
        }

        public static async Task<Ok<List<MetadataDto>>> getCategoryList(IGlobalServiceService globalServiceService)
        {
            var result = await globalServiceService.GetCategoryList();
            return TypedResults.Ok(result);
        }
    }
}
