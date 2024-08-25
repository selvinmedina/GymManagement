using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.ListGymsQuery
{
    public class ListGymsQueryHandler(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository)
        : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
    {
        public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery query, CancellationToken cancellationToken)
        {
            if(!await subscriptionsRepository.ExistsAsync(query.SubscriptionId))
            {
                return Error.NotFound("Subscription not found");
            }

            var gyms = await gymsRepository.ListBySubscriptionIdAsync(query.SubscriptionId);

            return gyms;
        }
    }
}
