using System;
using System.ComponentModel.DataAnnotations;

namespace ncorep.Models;

public class OrderDetail : IEntityBase
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    [DataType(DataType.Currency)] public decimal UnitCost { get; set; }

    public decimal? LineItemTotal { get; set; }

    [Required] public int OrderId { get; set; }

    public Order Order { get; set; }

    [Required] public int ProductId { get; set; }

    public Product Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}