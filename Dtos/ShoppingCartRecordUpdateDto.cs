using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class ShoppingCartRecordUpdateDto
{
    [Required] public int Id { get; set; }

    [Required] public int Quantity { get; set; }
}