using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ncorep.Models;

public class RefreshToken
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public string Token { get; set; }

    public string JwtId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpriryDate { get; set; }
    public bool Invalidated { get; set; }
    public bool Used { get; set; }
    public string UserId { get; set; }

    public AppUser User { get; set; }
}