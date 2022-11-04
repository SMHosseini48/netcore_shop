using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class ChangeEmailDto
{
    [Required(ErrorMessage = "Email address not specified")]
    [EmailAddress]
    public string NewEmail { get; set; }
}