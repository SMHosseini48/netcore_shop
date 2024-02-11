using System;

namespace ncorep.Models;

public interface IBaseEntity
{
    DateTime CreatedOn { get; set; }
    DateTime ModifiedOn { get; set; }
    bool Deletable { get; set; }
    bool IsDeleted { get; set; }
}

public interface IBaseEntity<TPrimaryKey> : IBaseEntity
{
    TPrimaryKey Id { get; set; }
}