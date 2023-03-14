using System;

namespace ncorep.Models;

public interface IEntityBase
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}