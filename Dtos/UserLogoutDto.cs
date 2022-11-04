using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class UserLogoutDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }
}