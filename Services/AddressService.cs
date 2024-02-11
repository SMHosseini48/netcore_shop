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

public class AddressService : IAddressService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddressService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResult> GetUserAddresses()
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type.ToString().Equals("id")).Value
            .ToString();
        var addresses = await _unitOfWork.Addresses.GetAllAsync(x => x.UserId.Equals(userId));
        var addressesDto = _mapper.Map<List<AddressDto>>(addresses);
        return new ServiceResult {Data = addressesDto, StatusCode = 200};
    }

    public async Task<ServiceResult> Create(AddressCreateDto addressCreateDto)
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type.ToString().Equals("id")).Value
            .ToString();
        var address = _mapper.Map<Address>(addressCreateDto);
        address.UserId = userId;
        await _unitOfWork.Addresses.InsertAsync(address);
        await _unitOfWork.Addresses.SaveChanges();

        var addressDto = _mapper.Map<AddressDto>(address);
        return new ServiceResult {Data = addressDto, StatusCode = 200};
    }

    public async Task<ServiceResult> Update(AddressUpdateDto addressUpdateDto)
    {
        var address = await _unitOfWork.Addresses.GetOneByQueryAsync(q => q.Id == addressUpdateDto.Id);
        if (address == null) return new ServiceResult {ErrorMessage = "address not found", StatusCode = 404};
        var userId = _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type.ToString().Equals("id")).Value
            .ToString();

        if (!address.UserId.Equals(userId))
            return new ServiceResult {ErrorMessage = "address doesnt belong to the user", StatusCode = 400};

        _mapper.Map(addressUpdateDto, address);
        _unitOfWork.Addresses.Update(address);
        await _unitOfWork.Addresses.SaveChanges();
        return new ServiceResult {Data = addressUpdateDto, StatusCode = 200};
    }

    public async Task<ServiceResult> Delete(string id)
    {
        var address = await _unitOfWork.Addresses.GetOneByQueryAsync(q => q.Id == id);
        if (address == null) return new ServiceResult {ErrorMessage = "address not found", StatusCode = 404};
        var userId = _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type.ToString().Equals("id")).Value
            .ToString();

        if (!address.UserId.Equals(userId))
            return new ServiceResult {ErrorMessage = "address doesnt belong to the user", StatusCode = 400};

        _unitOfWork.Addresses.Delete(address);
        await _unitOfWork.Addresses.SaveChanges();
        return new ServiceResult {Data = id, StatusCode = 200};
    }
}