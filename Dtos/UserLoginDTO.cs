using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class UserLoginDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}