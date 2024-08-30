using GymManagement.Domain.Subscriptions;

namespace TestCommon.TestsConstants
{
    public static partial class Constants
    {
        public static class Subscriptions
        {
            public static readonly SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
            public static readonly Guid Id = Guid.NewGuid();
            public const int MaxSessionsFreeTier = 3;
            public const int MaxRomsFreeTier = 1;
            public const int MaxGymsFreeTier = 1;
        }
    }
}
