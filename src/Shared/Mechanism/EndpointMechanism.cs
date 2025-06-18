 
using Shared.GenericRootModule.Features;

namespace Shared.Mechanism;

public interface MEndpointPostConfiguration;

public interface MEndpointPostConfiguration<T, V> : MEndpointPostConfiguration
where T : notnull
{
    public record Command(T input) : ICommand<GenericResult<V>>, IRequest<GenericResult<V>>;

    public interface IMEndpointPostHandler : ICommandHandler<Command, GenericResult<V>>;

    public abstract class MPostEndpoint(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericPostEndpoint<T, V>(endpoint, ActionName, serviceNames);

    public abstract class MValidator : AbstractValidator<Command>;
}

public interface MEndpointDeleteConfiguration<T, V>
where T : notnull
{
    public record Command(T input) : ICommand<GenericResult<V>>, IRequest<GenericResult<V>>;

    public interface IMEndpointDeleteHandler : ICommandHandler<Command, GenericResult<V>>;

    public abstract class MDeleteEndpoint(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericDeleteEndpoint<T, V>(endpoint, ActionName, serviceNames);
    public abstract class MValidator : AbstractValidator<Command>;
}
public interface MEndpointPutConfiguration<T, V> 
where T : notnull
{
    public record Command(T input) : ICommand<GenericResult<V>>, IRequest<GenericResult<V>>;

    public interface IMEndpointPutHandler : ICommandHandler<Command, GenericResult<V>>;

    public abstract class MPutEndpoint(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericPutEndpoint<T, V>(endpoint, ActionName, serviceNames);
    public abstract class MValidator : AbstractValidator<Command>;
}
public interface MEndpointGetConfiguration<T, V> 
where T : notnull
{
    public record Query(T input) : IQuery<GenericResult<V>>, IRequest<GenericResult<V>>;

    public interface IMEndpointGetHandler: IQueryHandler<Query, GenericResult<V>>;
    
    // Then, MEndpointGetRequest can inherit from this base class.
    public abstract class MGetEndpoint(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericGetEndpoint<T, V>(endpoint, ActionName, serviceNames);
    public abstract class MValidator : AbstractValidator<Query>;
}
