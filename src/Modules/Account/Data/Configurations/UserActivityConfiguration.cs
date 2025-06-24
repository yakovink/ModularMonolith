

namespace Account.Data.Configurations
{
    public class UserActivityConfiguration : AccountModuleStructre.IActivityConfigurations.IMDataConfiguration
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(u=>u.ActivityType).IsRequired();
            builder.Property(u=>u.UserId).IsRequired();
            builder.Property(u=>u.IpAddress).IsRequired();
            builder.Property(u=>u.isValidated).IsRequired();
            builder.HasOne(i => i.user).WithMany(u=>u.Activities).HasForeignKey(i=>i.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

/*
        public Guid UserId { get; private set; }
        public User user { get; private set; } = default!;
        public string ActivityType { get; private set; } = default!;
        public string IpAddress { get; private set; } = default!;
        public bool isValidated { get; private set; } = false;*/