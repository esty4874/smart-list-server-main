using SmartList.Contracts;

namespace smartList.Routes
{
    public static class UserRoutes
    {
        public static void MapUserRoutes(this IEndpointRouteBuilder app)
        {

            var userGroup = app.MapGroup("api/user")
                .WithTags("User");

            userGroup.MapPost("create", createUser)
                .WithName(nameof(createUser))
            .Produces<UserDto>(200);
            userGroup.MapPost("login", login)
                            .WithName(nameof(login))
                        .Produces<UserDto>(200);


        }

        public static async Task<Ok<UserDto>> login(string email, IUserService userService)
        {
            var result = await userService.Login(email);
            return TypedResults.Ok(result);
        }
        public static async Task<Ok<UserDto>> createUser(UserDto user, IUserService userService)
        {
            var result = await userService.CreateUser(user);
            return TypedResults.Ok(result);
        }

        public static async Task<Ok<List<MetadataDto>>> getCategoryList(IGlobalServiceService globalServiceService)
        {
            var result = await globalServiceService.GetCategoryList();
            return TypedResults.Ok(result);
        }
    }
}
