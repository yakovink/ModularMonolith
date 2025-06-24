 


namespace Shared.Mechanism;

public interface MModelConfiguration;
public interface Property<Model>;
public interface MEntityConfiguration;


public interface MModelConfiguration<Model> : MModelConfiguration, MEntityConfiguration<Model>
where Model : Aggregate<Guid>

{

    // dbcontext
    public abstract class MContext<MC>(DbContextOptions<MC> options, Type[] types) : GenericDbContext<MC>(options, types) where MC : GenericDbContext<MC>;


    //repositories
    public interface IMRepository;

    public abstract class LocalRepository<MC>(IGenericRepository<Model> repository) : GenericLocalRepository<MC, Model>(repository);


    //properties
    public interface Property : Property<Model>, IEntity<Guid>;
    
    public interface IMPropertyConfiguration<P> : MEntityConfiguration<P> where P : class, Property;
    
}

    public interface MEntityConfiguration<P> : MEntityConfiguration
    where P : class, IEntity<Guid>
    {
        public abstract record MDto;
        public interface IMDataConfiguration : IEntityTypeConfiguration<P>;
    }


