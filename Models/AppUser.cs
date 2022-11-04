using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ncorep.Models;

public class AppUser : IdentityUser<int>
{
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }

}