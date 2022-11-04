namespace ncorep.Dtos;

public class ShoppingCartRecordDTO
{
    public int Id { get; set; }

    public int Quantity { get; set; }
    public decimal LineItemTotal { get; set; }
    public ProductDto Product { get; set; }
}