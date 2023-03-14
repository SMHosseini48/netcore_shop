using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ncorep.Data;
using ncorep.Helpers;
using ncorep.Interfaces.Business;
using ncorep.Models;
using ncorep.Services;

namespace ncorep.Extensions;

public static class ServiceExtensions
{
    public static TokenValidationParameters ValidationParameters(IConfiguration configuration, bool expired)
    {
        var aliveTokenValidation = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JWT:Issuer"],
            ValidAudience = configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero
        };

        var expiredTokenValidation = aliveTokenValidation;
        expiredTokenValidation.ValidateLifetime = false;


        return expired ? expiredTokenValidation : aliveTokenValidation;
    }

    public static void ConfigureSqlConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EshopContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("MyConnectionString")));
    }

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var aliveTokenValidation = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JWT:Issuer"],
            ValidAudience = configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero
        };
        

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = ValidationParameters(configuration , true);
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole<int>>(q =>
            {
                q.User.RequireUniqueEmail = true;
                q.Password.RequiredLength = 2;
                q.Password.RequireDigit = false;
                q.Password.RequireNonAlphanumeric = false;
                q.Password.RequireUppercase = false;
                q.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<EshopContext>()
            .AddDefaultTokenProviders();
    }

    public static void ServiceInjection(this IServiceCollection services)
    {
        services.AddTransient<IJwtService, JwtService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ICartService, CartService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IAddressService, AddressService>();
        services.AddTransient<IImageService, ImageService>();
    }

    public static void SwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "E-Shop",
                    Description = "An ASP.NET Core Web API for Shopping"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            }
        );
    }
}