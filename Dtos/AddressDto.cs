using ncorep.Models;

namespace ncorep.Dtos;

public class AddressDto
{
    public long Id { get; set; }

    public string City { get; set; }

    public string StreetAddress { get; set; }

    public string Description { get; set; }

    public string PostalCode { get; set; }

    public int CustomerId { get; set; }
    
    public CustomerDto Customer { get; set; }
}