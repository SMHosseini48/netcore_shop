using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Data;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IGenericRepository<AppUser> users, IGenericRepository<ProductCategory> productCategories,
        IGenericRepository<RefreshToken> refreshTokens, IGenericRepository<Address> addresses,
        IGenericRepository<Product> products, IGenericRepository<Category> categories, IGenericRepository<Order> orders)
    {
        Users = users;
        RefreshTokens = refreshTokens;
        Addresses = addresses;
        Products = products;
        Categories = categories;
        Orders = orders;
        ProductCategories = productCategories;
    }

    public IGenericRepository<AppUser> Users { get; }
    public IGenericRepository<RefreshToken> RefreshTokens { get; }
    public IGenericRepository<Address> Addresses { get; }
    public IGenericRepository<Category> Categories { get; }
    public IGenericRepository<ProductCategory> ProductCategories { get; }
    public IGenericRepository<Product> Products { get; }
    public IGenericRepository<Order> Orders { get; }
}