 

namespace Shared.DDD;

public abstract class Entity<T> : IEntity<T>
{
    public required T Id { get; set; }
    public DateTime? _createdDate { get; set; }
    public DateTime? _lastModifiedDate { get; set; }
    public string? _createdBy { get; set; }
    public string? _lastModifiedBy { get; set; }
}
