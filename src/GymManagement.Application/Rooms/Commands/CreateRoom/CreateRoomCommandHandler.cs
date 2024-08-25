using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using MediatR;

namespace GymManagement.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandHandler(
        ISubscriptionsRepository subscriptionsRepository,
        IGymsRepository gymsRepository,
        IUnitOfWork unitOfWork)
            : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository = subscriptionsRepository;
        private readonly IGymsRepository _gymsRepository = gymsRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var gym = await _gymsRepository.GetGymByIdAsync(request.GymId);

            if (gym == null)
            {
                return Error.NotFound(description: "Gym not found");
            }

            var subscription = await _subscriptionsRepository.GetByIdAsync(gym.SubscriptionId);

            if (subscription == null)
            {
                return Error.NotFound(description: "Subscription not found");
            }

            var room = new Room(request.RoomName, gym.Id, maxDailySessions: subscription.GetMaxDailySessions());

            var addRoomResult = gym.AddRoom(room);

            if (addRoomResult.IsError)
            {
                return addRoomResult.Errors;
            }

            await _gymsRepository.UpdateGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();

            return room;
        }
    }
}
