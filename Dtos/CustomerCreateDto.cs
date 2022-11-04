using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class CustomerCreateDto
{
    [Required]
    [DataType(DataType.Text)]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [DataType(DataType.Text)]
    [MaxLength(50)]
    public string LastName { get; set; }
}