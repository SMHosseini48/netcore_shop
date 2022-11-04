using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Services;

public class CustomerService : ICustomerService
{
    private readonly IGenericRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly UserManager<AppUser> _usermanager;


    public CustomerService(IGenericRepository<Customer> customerRepository, IMapper mapper, IUserService userService ,UserManager<AppUser> usermanager)
    {
        _customerRepository = customerRepository;
        _usermanager = usermanager;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<ServiceResult> Create(int userId, UserRegisterDto userRegisterDto)
    {
        var customer = _mapper.Map<Customer>(userRegisterDto);
        customer.AppUserId = userId;

        await _customerRepository.InsertAsync(customer);
        await _customerRepository.SaveChanges();
        var userResponse = _mapper.Map<UserBaseResponseDto>(customer);
        return new ServiceResult {Data = userResponse, StatusCode = 200};
    }

    public async Task<ServiceResult> CustomerProfile()
    {
        var userSearchResult = await _userService.Find();
        if (userSearchResult.Data == null) return new ServiceResult {ErrorMessage = "user not found", StatusCode = 404};
        var customer = await _customerRepository.GetOneByQueryAsync(q => q.User == userSearchResult.Data , includes: new List<string> {"Addresses" , "Orders" , "ShoppingCartRecords"});
        if (customer == null) return new ServiceResult {ErrorMessage = "the user is not a customer", StatusCode = 404};

        var customerDto = _mapper.Map<CustomerDto>(customer);

        return new ServiceResult {Data = customerDto, StatusCode = 202};
    }

    public async Task<ServiceResult> ProfileUpdate(CustomerUpdateDto customerUpdateDto)
    {

        var customer = await _customerRepository.GetOneByQueryAsync(q => q.Id == customerUpdateDto.CustomerId);
        if (customer == null) return new ServiceResult {ErrorMessage = "customer not found", StatusCode = 404};
        _mapper.Map(customerUpdateDto, customer);
        var user = await _usermanager.FindByIdAsync(customer.AppUserId.ToString());
        _mapper.Map(customerUpdateDto, user);
        _customerRepository.Update(customer);
        await _usermanager.UpdateAsync(user);
        await _customerRepository.SaveChanges();

        var customerDto = _mapper.Map<CustomerDto>(customer);
        return new ServiceResult {Data = customerDto, StatusCode = 200};
    }
    
}