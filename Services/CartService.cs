using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Services;

public class CartService : ICartService
{
    private readonly IGenericRepository<ShoppingCartRecord> _shoppingCartRecordRepository;
    private readonly IGenericRepository<AppUser> _customerRepository;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public CartService(IGenericRepository<ShoppingCartRecord> shoppingCartRecordRepository,
        IGenericRepository<AppUser> customerRepository, IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _shoppingCartRecordRepository = shoppingCartRecordRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult> GetCart(int customerId)
    {
        var cart = await GetAllUserRecords(customerId);
        if (cart.Count < 0) return new ServiceResult {ErrorMessage = "cart is empty", StatusCode = 404};


        var cartDto = new ShoppingCartDTO
            {CustomerId = customerId, CartItems = _mapper.Map<List<ShoppingCartRecordDTO>>(cart)};

        return new ServiceResult {Data = cartDto, StatusCode = 200};
    }

    public async Task<ServiceResult> AddToCart(ShoppingCartRecordCreateDto item)
    {
        var customer =
            await _customerRepository.GetOneByQueryAsync(q => q.Id == item.CustomerId);
        if (customer == null) return new ServiceResult {ErrorMessage = "customer not found", StatusCode = 404};
        var product = await _productRepository.GetOneByQueryAsync(q => q.Id == item.ProductId);
        if (product == null) return new ServiceResult {ErrorMessage = "product not found", StatusCode = 404};

        var shoppingCartRecord = await _shoppingCartRecordRepository
            .GetOneByQueryAsync(q =>
                q.CustomerId == item.CustomerId &&
                q.ProductId == item.ProductId);
        if (shoppingCartRecord == null)
        {
            shoppingCartRecord = _mapper.Map<ShoppingCartRecord>(item);
            shoppingCartRecord.Quantity = 1;
            shoppingCartRecord.LineItemTotal = product.CurrentPrice;

            await _shoppingCartRecordRepository.InsertAsync(shoppingCartRecord);
        }
        else
        {
            shoppingCartRecord.Quantity += 1;
            shoppingCartRecord.LineItemTotal = shoppingCartRecord.Quantity * product.CurrentPrice;
            _shoppingCartRecordRepository.Update(shoppingCartRecord);
        }

        await _shoppingCartRecordRepository.SaveChanges();

        var cart = await GetAllUserRecords(customer.Id);
        var cartDto = new ShoppingCartDTO
            {CustomerId = customer.Id, CartItems = _mapper.Map<List<ShoppingCartRecordDTO>>(cart)};

        return new ServiceResult {Data = cartDto, StatusCode = 200};
    }

    public async Task<ServiceResult> DeleteItem(int itemId)
    {
        var item = await _shoppingCartRecordRepository.GetOneByQueryAsync(q => q.Id == itemId);
        if (item == null) return new ServiceResult {ErrorMessage = "cart item not found", StatusCode = 404};

        item.Quantity -= 1;
        item.LineItemTotal = item.Quantity * item.Product.CurrentPrice;
        _shoppingCartRecordRepository.Update(item);
        await _shoppingCartRecordRepository.SaveChanges();

        var cart = await GetAllUserRecords(item.CustomerId);
        var cartDto = new ShoppingCartDTO
            {CustomerId = item.CustomerId, CartItems = _mapper.Map<List<ShoppingCartRecordDTO>>(cart)};

        return new ServiceResult {Data = cartDto, StatusCode = 200};
    }

    public async Task<ServiceResult> UpdateCartItem(ShoppingCartRecordUpdateDto itemUpdate)
    {
        var item = await _shoppingCartRecordRepository.GetOneByQueryAsync(q => q.Id == itemUpdate.Id ,includes: new List<string> {"Product"});
        if (item == null) return new ServiceResult {ErrorMessage = "cart item not found", StatusCode = 404};

        _mapper.Map(itemUpdate, item); //quantity update
        item.LineItemTotal = item.Quantity * item.Product.CurrentPrice;

        _shoppingCartRecordRepository.Update(item);
        await _shoppingCartRecordRepository.SaveChanges();

        var cart = await GetAllUserRecords(item.CustomerId);
        var cartDto = new ShoppingCartDTO
            {CustomerId = item.CustomerId, CartItems = _mapper.Map<List<ShoppingCartRecordDTO>>(cart)};

        return new ServiceResult {Data = cartDto, StatusCode = 200};
    }

    public async Task<ServiceResult> DeleteCart(int customerId)
    {
        var customer = await _customerRepository.GetOneByQueryAsync(q => q.Id == customerId);
        if (customer == null) return new ServiceResult {ErrorMessage = "customer not found", StatusCode = 404};

        var cart = await GetAllUserRecords(customerId);
        if (cart.Count < 1) return new ServiceResult {ErrorMessage = "cart is empty", StatusCode = 404};

        _shoppingCartRecordRepository.DeleteRange(cart);
        await _shoppingCartRecordRepository.SaveChanges();

        return new ServiceResult {Data = customerId, StatusCode = 200};
    }

    private async Task<IList<ShoppingCartRecord>> GetAllUserRecords(int customerId)
    {
        var items = await _shoppingCartRecordRepository.GetAllAsync(q => q.CustomerId == customerId , includes: new List<string> {"Product"});
        if (items.Count < 0) return null;
        foreach (var item in items)
            item.LineItemTotal = item.Quantity * item.Product.CurrentPrice;
        return items;
    }
}