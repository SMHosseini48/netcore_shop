using System.Collections.Generic;
using ncorep.Models;

namespace ncorep.Dtos;

public class CustomerDto : UserBaseResponseDto
{
    public List<AddressDto> Addresses { get; set; }

    public List<OrderDTO> Orders { get; set; }

    public List<ShoppingCartRecordDTO> ShoppingCartRecords { get; set; }
}