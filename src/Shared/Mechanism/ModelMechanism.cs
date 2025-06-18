 
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Data;

namespace Shared.Mechanism;

public interface MModelConfiguration;
public interface Property<Model>;
public interface MEntityConfiguration;


public interface MModelConfiguration<Model> : MModelConfiguration, MEntityConfiguration<Model>
where Model : class, IAggregate<Guid>

{

    public abstract class MContext<MC>(DbContextOptions<MC> options, Type[] types) : GenericDbContext<MC>(options, types) where MC : GenericDbContext<MC>;

    public interface IMRepository;
    public abstract class MRepository<MC>(MC context) : GenericRepository<Model, MC>(context), IMRepository where MC : GenericDbContext<MC>;

    public abstract class MCachedRepository<MC>(MRepository<MC> repository, IDistributedCache cache) : GenericCachedRepository<Model, MC>(repository, cache), IMRepository where MC : GenericDbContext<MC>;

    public interface Property : Property<Model>, IEntity<Guid>;
    
    public interface IMPropertyConfiguration<P> : MEntityConfiguration<P> where P : class, Property;
    
}

    public interface MEntityConfiguration<P> : MEntityConfiguration
    where P : class, IEntity<Guid>
    {
        public abstract record MDto;
        public interface IMDataConfiguration : IEntityTypeConfiguration<P>;
    }


