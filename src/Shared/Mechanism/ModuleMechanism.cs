




using Microsoft.Extensions.Caching.Distributed;
using Shared.Data;

namespace Shared.Mechanism;

public abstract class ModuleMechanism <Model> where Model : Aggregate<Guid>
{   

    // applyment
    public interface IMModelConfiguration : MModelConfiguration<Model>;
    
    //API
    public interface MPost<T,V> : MEndpointPostConfiguration<T,V> where T : notnull;
    public interface MDelete<T,V> : MEndpointDeleteConfiguration<T,V> where T : notnull;


    public interface MPut<T,V> : MEndpointPutConfiguration<T,V> where T : notnull;


    public interface MGet<T, V> : MEndpointGetConfiguration<T, V> where T : notnull;
}

