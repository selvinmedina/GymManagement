using ErrorOr;
using GymManagement.Domain.Rooms;
using Throw;

namespace GymManagement.Domain.Gyms
{
    public class Gym
    {
        private readonly int _maxRooms;

        public Guid Id { get; }
        private readonly List<Guid> _roomsIds = [];
        private readonly List<Guid> _trainerIds = [];

        public string Name { get; init; } = null!;
        public Guid SubscriptionId { get; init; }

        public Gym(
            string name,
            int maxRooms,
            Guid subscriptonId,
            Guid? id = null)
        {
            Name = name;
            _maxRooms = maxRooms;
            SubscriptionId = subscriptonId;
            Id = id ?? Guid.NewGuid();
        }

        private Gym()
        {
        }

        public ErrorOr<Success> AddTrainer(Guid trainerId)
        {
            if (_trainerIds.Contains(trainerId))
            {
                return Error.Conflict(description: "Trainer already exists in gym");
            }

            _trainerIds.Add(trainerId);

            return Result.Success;
        }

        public ErrorOr<Success> AddRoom(Room room)
        {
            _roomsIds.Throw().IfContains(room.Id);

            if (_roomsIds.Count >= _maxRooms)
            {
                return Error.Conflict(description: "Cannot have more rooms than the gym allows");
            }

            _roomsIds.Add(room.Id);

            return Result.Success;
        }

        public bool HasRoom(Guid roomId)
        {
            return _roomsIds.Contains(roomId);
        }

        public void RemoveRoom(Guid roomId)
        {
            _roomsIds.Remove(roomId);
        }
    }
}
