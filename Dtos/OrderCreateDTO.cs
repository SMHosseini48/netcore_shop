using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class OrderCreateDTO
{
    [Required] public int CustomerId { get; set; }
}