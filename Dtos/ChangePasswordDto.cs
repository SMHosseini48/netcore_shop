using System.ComponentModel.DataAnnotations;

namespace ncorep.Dtos;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "current password not specified")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "a new password not specified")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "the new password confirmation is required")]
    public string ConfirmNewPassword { get; set; }
}