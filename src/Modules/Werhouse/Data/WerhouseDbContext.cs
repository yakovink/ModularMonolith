using System;
using Werhouse.Items.Models;

namespace Werhouse.Data;

public class WerhouseDbContext: DbContext
{
    public WerhouseDbContext(DbContextOptions<WerhouseDbContext> options) : base(options)
    {
    }

    public DbSet<WerhouseItem> ShoppingCarts => Set<WerhouseItem>();
    public DbSet<WerhouseItemHistory> ShoppingCartItems => Set<WerhouseItemHistory>();

    protected override void OnModelCreating(ModelBuilder builder)
    {


        builder.HasDefaultSchema("werhouse");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
