using System;
using System.Collections.Generic;

namespace ncorep.Dtos;

public class OrderDTO
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime ShipDate { get; set; }

    public CustomerDto Customer { get; set; }

    public List<OrderDetailDTO> OrderDetails { get; set; } = new();
}