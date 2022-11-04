using System;
using System.Collections.Generic;
using ncorep.Models;

namespace ncorep.Dtos;

public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}