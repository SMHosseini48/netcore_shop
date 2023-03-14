using System;
using System.ComponentModel.DataAnnotations;

namespace ncorep.Models;

public class Address : IEntityBase
{
    public long Id { get; set; }

    [Required] [MaxLength(150)] public string City { get; set; }

    [Required] [MaxLength(200)] public string StreetAddress { get; set; }

    [Required] [MaxLength(250)] public string Description { get; set; }

    [Required]
    [MaxLength(50)]
    [DataType(DataType.PostalCode)]
    public string PostalCode { get; set; }

    public int UserId { get; set; }

    public AppUser User { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}