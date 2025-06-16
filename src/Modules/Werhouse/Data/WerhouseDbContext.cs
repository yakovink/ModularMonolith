
namespace Werhouse.Data;

public class WerhouseDbContext : WerhouseModuleStructre.MWerhouseDbContext
{
    public WerhouseDbContext(DbContextOptions<WerhouseDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("werhouse");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }


    
}
