using System.Collections.Generic;

namespace ncorep.Dtos;

public class CustomerDto : AuthenticationResultDto
{
    public List<AddressDto> Addresses { get; set; }

    public List<OrderDTO> Orders { get; set; }

    public List<ShoppingCartRecordDTO> ShoppingCartRecords { get; set; }
}