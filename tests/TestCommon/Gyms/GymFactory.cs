using GymManagement.Domain.Gyms;
using TestCommon.TestsConstants;

namespace TestCommon.Gyms
{
    public static class GymFactory
    {
        public static Gym CreateGym(
            string name = Constants.Gym.Name,
            int maxRooms = Constants.Subscriptions.MaxRomsFreeTier,
            Guid? id = null
            )
        {
            return new Gym(
                name: name,
                maxRooms,
                subscriptonId: Constants.Subscriptions.Id,
                id ?? Constants.Gym.Id
                );
        }
    }
}
