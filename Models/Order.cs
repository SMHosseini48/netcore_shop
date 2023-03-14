using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ncorep.Models;

public class Order : IEntityBase
{
    public int Id { get; set; }

    [DataType(DataType.Date)] public DateTime OrderDate { get; set; }

    [DataType(DataType.Date)] public DateTime ShipDate { get; set; }

    public int CustomerId { get; set; }

    public AppUser Customer { get; set; }

    public IList<OrderDetail> OrderDetails { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}