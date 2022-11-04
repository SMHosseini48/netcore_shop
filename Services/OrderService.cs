using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Services;

public class OrderService : IOrderService
{
    private readonly IGenericRepository<Order> _orderRepository;
    private readonly IGenericRepository<Customer> _customerRepository;
    private readonly IGenericRepository<ShoppingCartRecord> _shoppingCartRepository;
    private readonly IMapper _mapper;

    public OrderService(IGenericRepository<Order> orderRepository,IGenericRepository<ShoppingCartRecord> shoppingCartRepository,IGenericRepository<Customer> customerRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _shoppingCartRepository = shoppingCartRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult> OrderRegister(OrderCreateDTO orderCreateDto)
    {
        var customer = await _customerRepository.GetOneByQueryAsync(q => q.Id == orderCreateDto.CustomerId);
        if (customer == null) return new ServiceResult {ErrorMessage = "customer not found", StatusCode = 404};

        var cartItems = await _shoppingCartRepository.GetAllAsync(q => q.CustomerId == orderCreateDto.CustomerId ,includes: new List<string> {"Product"});
        if (cartItems.Count < 1) return new ServiceResult {ErrorMessage = "cart is empty", StatusCode = 404};
        
        var order = new Order
        {
            CustomerId = customer.Id
        };

        List<OrderDetail> orderDetails = new();
        foreach (var item in cartItems)
        {
            var orderDetail = new OrderDetail
            {
                Quantity = item.Quantity,
                ProductId = item.Product.Id,
                Order = order,
                UnitCost = item.Product.CurrentPrice
            };
            orderDetails.Add(orderDetail);
        }

        order.OrderDetails = orderDetails;

        await _orderRepository.InsertAsync(order);
        _shoppingCartRepository.DeleteRange(cartItems);
        await _orderRepository.SaveChanges();
        await _shoppingCartRepository.SaveChanges();

        var result = _mapper.Map<OrderDTO>(order);

        return new ServiceResult {Data = result, StatusCode = 200};
    }
    
}