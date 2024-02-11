using System.Collections.Generic;

namespace ncorep.Dtos;

public class GetProductList
{
    public IEnumerable<string>? CategoriesId { get; set; }
}