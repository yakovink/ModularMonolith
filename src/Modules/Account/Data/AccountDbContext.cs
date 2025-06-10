
namespace Account.Data;

public class AccountDbContext : DbContext
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("account");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
        // Configure your entities here
    }

    public async Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken, RequestType type)
    {
        User? user = null;
        // Get the user entity by ID
        if (type == RequestType.Query)
        {
            user = await Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id==id, cancellationToken);
        }
        else if (type == RequestType.Command)
        {
            user = await Users.FindAsync(new object[] { id }, cancellationToken);
        }

        if (user == null)
        {
            throw new Exception($"User not found with ID: {id}");
        }
        return user;
    }
    



}
