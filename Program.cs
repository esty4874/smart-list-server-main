using SmartList.Contracts;
using SmartList.Services;
using smartList.Routes;
using smartList;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()

            );
});
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<SmartListContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("smartListConnection")));
builder.Services.AddScoped<IProductsServiceService, ProductsService>();
builder.Services.AddScoped<IGlobalServiceService, GlobalService>();
builder.Services.AddScoped<IShopListService, ShopListService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "App v1",
        Version = "v1"
    });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapProductsRoutes();
app.MapGlobalRoutes();
app.MapShopListRoutes();
app.MapUserRoutes();
app.Run();
