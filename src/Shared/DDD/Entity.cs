 

namespace Shared.DDD;

public abstract class Entity<T> : IEntity<T>
{
    public required T Id { get; set; }
    public DateTime? _createdDate { get; set; } = DateTime.UtcNow;
    public DateTime? _lastModifiedDate { get; set; } = DateTime.UtcNow;
    public string? _createdBy { get; set; } = Environment.UserName;
    public string? _lastModifiedBy { get; set; } = Environment.UserName;
}
