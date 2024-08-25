using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription
{
    public record CreateSubscriptionCommandHandler(
            ISubscriptionsRepository subscriptionsRepository,
            IUnitOfWork unitOfWork,
            IAdminsRepository adminsRepository
            ) : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository = subscriptionsRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IAdminsRepository _adminsRepository = adminsRepository;

        public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminsRepository.GetByIdAsync(request.AdminId);

            if (admin is null)
            {
                return Error.NotFound(description: "Admin not found");
            }

            if (admin.SubscriptionId is not null)
            {
                return Error.Conflict(description: "Admin already has an active subscription");
            }

            // create a subscription
            var subscription = new Subscription(subscriptionType: request.SubscriptionType, adminId: request.AdminId);

            admin.SetSubscription(subscription);

            // add it to the database
            await _subscriptionsRepository.AddSubscriptionAsync(subscription);
            await _adminsRepository.UpdateAsync(admin);

            // save the changes
            await _unitOfWork.CommitChangesAsync();

            // return the subscription
            return subscription;
        }
    }
}
