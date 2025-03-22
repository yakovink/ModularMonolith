using System;

namespace Shared.DDD;

public interface IEntity
{

    public DateTime? _createdDate  { get; set; }
    public DateTime? _lastModifiedDate { get; set; }
    public string? _createdBy { get; set; }
    public string? _lastModifiedBy { get; set; }

}

public interface IEntity<T> : IEntity
{
    public T Id { get; set; }
}
