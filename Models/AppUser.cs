using System.Collections.Generic;

namespace ncorep.Models;

public class
    AppUser : BaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public IList<Address> Addresses { get; set; }

    public IList<Order> Orders { get; set; }

    public bool IsAuthenticated { get; set; }
}