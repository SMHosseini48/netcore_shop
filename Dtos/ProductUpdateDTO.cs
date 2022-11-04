using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class ProductUpdateDTO : ProductCreateDto 
{
    [Required] public int Id { get; set; }
}