using System;
using Shared.Data;
using Werhouse.Items.Models;

namespace Werhouse.Data;

public class WerhouseDbContext : GenericDbContext<WerhouseDbContext>
{
    public WerhouseDbContext(DbContextOptions<WerhouseDbContext> options)
        : base(options, new[]{
            typeof(WerhouseItemHistory),
            typeof(WerhouseItem)
        })
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("werhouse");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }


    
}
