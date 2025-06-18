
namespace Werhouse.Data;

public class WerhouseDbContext(DbContextOptions<WerhouseDbContext> options) : WerhouseModuleStructre.MWerhouseDbContext(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("werhouse");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }


    
}
