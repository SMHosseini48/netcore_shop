using System.Collections.Generic;
using ncorep.Models;

namespace ncorep.Dtos;

public class ProductDto : ProductCreateDto
{
    public int Id { get; set; }
    
    public CategoryDTO Category { get; set; }

    public List<OrderDetailDTO> OrderDetails { get; set; }

    public List<ImageDto> Images { get; set; }
}