using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class CustomerUpdateDto
{
    [Required] public int CustomerId { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [DataType(DataType.Text)]
    [MaxLength(50)]
    public string LastName { get; set; }
}