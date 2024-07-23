using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//own services
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//services for shopping cart
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

//adding MVC framework
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        //ignore cycles when serializing
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    ;

//adding razor pages framework
builder.Services.AddRazorPages();

builder.Services.AddDbContext<BethanysPieShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]);
});

var app = builder.Build();

//Adding middleware components here:
app.UseStaticFiles();
app.UseSession();

//authentication middleware
app.UseAuthentication();

//catch errors if there are any
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//endpoint middleware: navigating pages or routes
app.MapDefaultControllerRoute();

//app.UseAntiforgery();

//enables razor pages
app.MapRazorPages();

DbInitializer.Seed(app);

app.Run();
