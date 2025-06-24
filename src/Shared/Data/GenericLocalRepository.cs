
namespace Shared.Data
{
    public abstract class GenericLocalRepository<MC,Model>(IGenericRepository<Model> repository)
    where Model : Aggregate<Guid>
    {

    }
}
