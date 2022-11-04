using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class AddressUpdateDto
{
    [Required] public int Id { get; set; }

    [Required] [MaxLength(150)] public string City { get; set; }

    [Required] [MaxLength(200)] public string StreetAddress { get; set; }

    [Required] [MaxLength(250)] public string Description { get; set; }

    [Required]
    [MaxLength(50)]
    [DataType(DataType.PostalCode)]
    public string PostalCode { get; set; }
}