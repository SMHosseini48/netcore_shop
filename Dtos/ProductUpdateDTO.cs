using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class ProductUpdateDTO : ProductCreateDto
{
    [Required] public string Id { get; set; }
}