using GymManagement.Domain.Subscriptions;
using TestCommon.TestsConstants;

namespace TestCommon.Subscriptions
{
    public static class SubscriptionFactory
    {
        public static Subscription CreateSubscription(
            SubscriptionType? subscriptionType = null,
            Guid? adminId = null,
            Guid? id = null
            )
        {
            return new Subscription(
                subscriptionType: subscriptionType ?? Constants.Subscriptions.DefaultSubscriptionType,
                adminId ?? Constants.Admin.Id,
                id ?? Constants.Subscriptions.Id
                );
        }
    }
}
