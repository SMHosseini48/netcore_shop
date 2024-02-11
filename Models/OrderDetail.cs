using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ncorep.Models;

public class OrderDetail : BaseEntity
{
    [Column(TypeName = "decimal(14, 5)")] public decimal LineItemTotal { get; set; }

    [Required] public string OrderId { get; set; }
    public Order Order { get; set; }
    [Required] public string ProductId { get; set; }

    public Product Product { get; set; }
}