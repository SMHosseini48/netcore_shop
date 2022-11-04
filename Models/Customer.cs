using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ncorep.Models;

public class Customer : EntityBase
{
    public int Id { get; set; }

    [Required]
    [InverseProperty("FirstName")]
    [DataType(DataType.Text)]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [InverseProperty("LastName")]
    [DataType(DataType.Text)]
    [MaxLength(50)]
    public string LastName { get; set; }

    
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public int AppUserId { get; set; }
    public AppUser User { get; set; }

    public IList<Address> Addresses { get; set; }

    public IList<Order> Orders { get; set; }

    public IList<ShoppingCartRecord> ShoppingCartRecords { get; set; }
}