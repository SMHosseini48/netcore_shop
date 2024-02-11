namespace ncorep.Models;

public class ProductCategory : BaseEntity
{
    public string ProductId { get; set; }
    public Product Product { get; set; }

    public string CategoryId { get; set; }
    public Category Category { get; set; }
}