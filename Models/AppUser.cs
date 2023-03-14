using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ncorep.Models;

public class AppUser : IdentityUser<int> ,IEntityBase
{
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }
    
    public IList<Address> Addresses { get; set; }

    public IList<Order> Orders { get; set; }

    public IList<ShoppingCartRecord> ShoppingCartRecords { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}