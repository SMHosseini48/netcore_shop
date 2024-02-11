using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class CategoryCreateDTO
{
    [Required(ErrorMessage = "Category name is required")]
    public string Name { get; set; }

    public string? ParentId { get; set; }
}