namespace GymManagement.Domain.Subscriptions
{
    public class Subscription
    {
        private readonly Guid _adminId;
        public Guid Id { get; set; }
        public SubscriptionType SubscriptionType { get; }

        public Subscription(
            Guid? id, 
            SubscriptionType subscriptionType,
            Guid adminId)
        {
            Id = id ?? Guid.NewGuid();
            SubscriptionType = subscriptionType;
            _adminId = adminId;
        }
    }
}
