using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ncorep.Models;

public class ShoppingCartRecord : IEntityBase
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    [NotMapped]
    [DataType(DataType.Currency)]
    public decimal LineItemTotal { get; set; }

    public int CustomerId { get; set; }

    public AppUser Customer { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}