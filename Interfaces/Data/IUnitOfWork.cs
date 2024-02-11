using ncorep.Models;

namespace ncorep.Interfaces.Data;

public interface IUnitOfWork
{
    IGenericRepository<AppUser> Users { get; }
    IGenericRepository<RefreshToken> RefreshTokens { get; }
    
    IGenericRepository<Category> Categories { get; }
    IGenericRepository<Address> Addresses { get; }
    IGenericRepository<ProductCategory> ProductCategories { get; }
    IGenericRepository<Product> Products { get; }
    
    IGenericRepository<Order> Orders { get; }
}