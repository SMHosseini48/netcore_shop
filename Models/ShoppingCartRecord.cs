using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ncorep.Models;

public class ShoppingCartRecord : BaseEntity
{
    public int Quantity { get; set; }

    [NotMapped]
    [DataType(DataType.Currency)]
    public decimal LineItemTotal { get; set; }

    public string UserId { get; set; }

    public AppUser User { get; set; }

    public string ProductId { get; set; }
    public Product Product { get; set; }
}