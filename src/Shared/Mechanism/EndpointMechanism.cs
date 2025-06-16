using System;
using Shared.GenericRootModule.Features;

namespace Shared.Mechanism;

public interface MEndpointPostConfiguration;

public interface MEndpointPostConfiguration<T, V> : MEndpointPostConfiguration
where T : notnull
{
    public interface MEndpointPostRequest : ICommand<GenericResult<V>>, IRequest<GenericResult<V>>;

    public interface IMEndpointPostHandler : ICommandHandler<MEndpointPostRequest, GenericResult<V>>;

    public abstract class MPostEndpoint(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericPostEndpoint<T, V>(endpoint, ActionName, serviceNames);

    public abstract class MValidator : AbstractValidator<MEndpointPostRequest>;
}

public interface MEndpointDeleteConfiguration<T, V>
where T : notnull
{
    public interface MEndpointDeleteRequest : ICommand<GenericResult<V>>, IRequest<GenericResult<V>>;

    public interface IMEndpointDeleteHandler : ICommandHandler<MEndpointDeleteRequest, GenericResult<V>>;

    public abstract class MDeleteEndpoint(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericDeleteEndpoint<T, V>(endpoint, ActionName, serviceNames);
    public abstract class MValidator : AbstractValidator<MEndpointDeleteRequest>;
}
public interface MEndpointPutConfiguration<T, V> 
where T : notnull
{
    public interface MEndpointPutRequest : ICommand<GenericResult<V>>, IRequest<GenericResult<V>>;

    public interface IMEndpointPutHandler : ICommandHandler<MEndpointPutRequest, GenericResult<V>>;

    public abstract class MPutEndpoint(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericPutEndpoint<T, V>(endpoint, ActionName, serviceNames);
    public abstract class MValidator : AbstractValidator<MEndpointPutRequest>;
}
public interface MEndpointGetConfiguration<T, V> 
where T : notnull
{
    public abstract record MEndpointGetRequest : IQuery<GenericResult<V>>, IRequest<GenericResult<V>>;

    public interface IMEndpointGetHandler<R> : IQueryHandler<R, GenericResult<V>> where R : MEndpointGetRequest;
    
    // Then, MEndpointGetRequest can inherit from this base class.
    public abstract class MGetEndpoint(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericGetEndpoint<T, V>(endpoint, ActionName, serviceNames);
    public abstract class MValidator : AbstractValidator<MEndpointGetRequest>;
}
