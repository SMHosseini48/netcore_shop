using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class ImageCreateDto
{
    [Required]
    public int ProductId { get; set; }
}