

namespace Account.Users.Models
{
    public class UserActivity : Entity<Guid>, AccountModuleStructre.IAccountConfigurations.Property
    {
        public Guid UserId { get; private set; }
        [JsonIgnore]
        public User user { get; private set; } = default!;
        public string ActivityType { get; private set; } = default!;
        public string IpAddress { get; private set; } = default!;
        public bool isValidated { get; private set; } = false;




        public static UserActivity Create(User user, bool isValidated, string activityType, string ipAddress)
        {
            if (string.IsNullOrEmpty(activityType))
            {
                throw new ArgumentException("Activity type cannot be null or empty", nameof(activityType));
            }
            if (string.IsNullOrEmpty(ipAddress))
            {
                throw new ArgumentException("IP address cannot be null or empty", nameof(ipAddress));
            }
            return new UserActivity
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                user = user,
                ActivityType = activityType,
                IpAddress = ipAddress,
                isValidated = isValidated
            };
        }
     }
}
