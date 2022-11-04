using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ncorep.Models;

public class Order : EntityBase
{
    public int Id { get; set; }

    [DataType(DataType.Date)] public DateTime OrderDate { get; set; }

    [DataType(DataType.Date)] public DateTime ShipDate { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; }

    public IList<OrderDetail> OrderDetails { get; set; }
}