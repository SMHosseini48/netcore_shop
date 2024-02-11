using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ncorep.Models;

[Index(nameof(Name), IsUnique = true)]
public class Category : BaseEntity
{
    [Required(ErrorMessage = "Category name is required")]
    [MaxLength(50)]
    public string Name { get; set; }


    public string ParentId { get; set; }
    public Category Parent { get; set; }

    public List<Category> Childrens { get; set; }

    public List<ProductCategory> ProductCategories { get; set; }
}