using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class OrderCreateDto
{
    public DateTime OrderDate { get; set; }

    public DateTime ShipDate { get; set; }
    
    public IList<OrderDetailDto> OrderDetails { get; set; }

}