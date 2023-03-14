using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ncorep.Dtos;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Services;

public class AddressService : IAddressService
{
    private readonly IGenericRepository<Address> _addressRepository;
    private readonly IGenericRepository<AppUser> _customerRepository;
    private readonly IMapper _mapper;

    public AddressService(IGenericRepository<Address> addressRepository,IGenericRepository<AppUser> customerRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult> GetOneById(int id)
    {
        var address = await _addressRepository.GetOneByQueryAsync(q => q.Id == id , includes: new List<string> {"Customer"});
        if (address == null) return new ServiceResult {ErrorMessage = "address not found", StatusCode = 404};
        var addressDto = _mapper.Map<AddressDto>(address);
        return new ServiceResult {Data = address, StatusCode = 200};
    }

    public async Task<ServiceResult> Create(AddressCreateDto addressCreateDto)
    {
        var customer = await _customerRepository.GetOneByQueryAsync(q => q.Id == addressCreateDto.CustomerId);
        if (customer == null) return new ServiceResult {ErrorMessage = "customer not found", StatusCode = 404};
        var address = _mapper.Map<Address>(addressCreateDto);
        await _addressRepository.InsertAsync(address);
        await _addressRepository.SaveChanges();
        
        var addressDto = _mapper.Map<AddressDto>(address);
        return new ServiceResult {Data = addressDto , StatusCode = 200};
    }

    public async Task<ServiceResult> Update(AddressUpdateDto addressUpdateDto)
    {
        var address = await _addressRepository.GetOneByQueryAsync(q => q.Id == addressUpdateDto.Id);
        if (address == null) return new ServiceResult {ErrorMessage = "address not found" , StatusCode = 404};
        _mapper.Map(addressUpdateDto, address);
        _addressRepository.Update(address);
        await _addressRepository.SaveChanges();
        return new ServiceResult {Data = addressUpdateDto , StatusCode = 200};
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var address = await _addressRepository.GetOneByQueryAsync(q => q.Id == id);
        if (address == null) return new ServiceResult {ErrorMessage = "address not found" , StatusCode = 404};
        _addressRepository.Delete(address);
        await _addressRepository.SaveChanges();
        return new ServiceResult {Data = id , StatusCode = 200};
    }
}