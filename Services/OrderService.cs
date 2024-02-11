using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Services;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private IHttpContextAccessor _httpContextAccessor;
    private IUnitOfWork _unitOfWork;
    public OrderService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult> OrderRegister(OrderCreateDto orderCreateDto)
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type.ToString().Equals("id")).Value
            .ToString();
        var order = new Order
        {
            UserId = userId,
            ShipDate = orderCreateDto.ShipDate,
            OrderDate = orderCreateDto.OrderDate
        };
        order.OrderDetails = new List<OrderDetail>();
        foreach (var item in orderCreateDto.OrderDetails)
        {
            order.OrderDetails.Add(new OrderDetail
            {
                ProductId = item.ProductId,
                LineItemTotal = item.LineItemTotal
            });
        }

        await _unitOfWork.Orders.InsertAsync(order);
        await _unitOfWork.Orders.SaveChanges();

        return new ServiceResult {Data = order.Id , StatusCode = 200};
    }
}