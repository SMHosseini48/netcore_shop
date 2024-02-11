using System;
using System.Collections.Generic;

namespace ncorep.Models;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; }

    public DateTime ShipDate { get; set; }

    public string UserId { get; set; }

    public AppUser User { get; set; }
    public IList<OrderDetail> OrderDetails { get; set; }
}