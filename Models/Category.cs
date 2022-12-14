using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ncorep.Models;

[Index(nameof(Name), IsUnique = true)]
public class Category : EntityBase
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [DataType(DataType.Text)]
    [MaxLength(50)]
    public string Name { get; set; }

    public IList<Product> Products { get; set; }
}