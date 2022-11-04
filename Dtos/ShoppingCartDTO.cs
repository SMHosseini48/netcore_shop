using System.Collections.Generic;

namespace ncorep.Dtos;

public class ShoppingCartDTO
{
    public int CustomerId { get; set; }

    public List<ShoppingCartRecordDTO> CartItems { get; set; }
}