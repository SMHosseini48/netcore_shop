using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class ShoppingCartRecordCreateDto
{
    [Required] public int CustomerId { get; set; }

    [Required] public int ProductId { get; set; }
}