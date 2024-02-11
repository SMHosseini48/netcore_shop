using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ncorep.Models;

public class Product : BaseEntity
{
    [Required(ErrorMessage = "Product name is required")]
    [MaxLength(250)]
    public string Name { get; set; }

    [MaxLength(3800)] public string Description { get; set; }

    [MaxLength(50)] public string ModelName { get; set; }

    [MaxLength(50)] public string ModelNumber { get; set; }
    [DataType(DataType.Currency)] public decimal UnitCost { get; set; }

    [DataType(DataType.Currency)] public decimal CurrentPrice { get; set; }

    public int UnitInStock { get; set; }

    public IList<ProductCategory> ProductCategories { get; set; }


    public IList<Image> Images { get; set; }
}