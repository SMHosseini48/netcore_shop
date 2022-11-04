using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class UserRegisterDto
{
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Password Confirmation is required")]
    public string ConfirmPassword { get; set; }
}