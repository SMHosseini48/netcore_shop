using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ncorep.Data;
using ncorep.Interfaces.Business;
using ncorep.Models;
using ncorep.Services;

namespace ncorep.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EshopContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("MyConnectionString")));
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole<int>>(q =>
            {
                q.User.RequireUniqueEmail = true;
                q.Password.RequiredLength = 8;
                q.Password.RequireDigit = true;
                q.Password.RequireNonAlphanumeric = false;
                q.Password.RequireUppercase = true;
                q.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<EshopContext>()
            .AddDefaultTokenProviders();
    }

    public static void ServiceInjection(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<ICartService, CartService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IAddressService, AddressService>();
        services.AddTransient<IImageService, ImageService>();
    }
}