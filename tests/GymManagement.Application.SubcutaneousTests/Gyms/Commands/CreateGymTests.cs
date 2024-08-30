using ErrorOr;
using FluentAssertions;
using GymManagement.Application.SubcutaneousTests.Common;
using GymManagement.Domain.Subscriptions;
using MediatR;
using TestCommon.Gyms;
using TestCommon.Subscriptions;

namespace GymManagement.Application.SubcutaneousTests.Gyms.Commands
{
    [Collection(MediatorFactoryCollection.CollectionName)]
    public class CreateGymTests(MediatorFactory mediatorFactory)
    {
        private readonly IMediator _mediator = mediatorFactory.CreateMediator();

        private async Task<Subscription> CreateSubscription()
        {
            // Arrange
            var createSubscriptionCommand = SubscriptionCommandFactory.CreateCreateSubscriptionCommand();

            // Act
            var result = await _mediator.Send(createSubscriptionCommand);

            // Assert
            result.IsError.Should().BeFalse();

            return result.Value;
        }

        [Fact]
        public async Task CreateGym_WhenValidCommand_ShouldCreateGym()
        {
            // Arrange
            var subscription = await CreateSubscription();
            var createGymCommand = GymCommandFactory.CreateCreateGymCommand(subscriptionId: subscription.Id);

            // Act
            var createGymResult = await _mediator.Send(createGymCommand);

            // Assert
            createGymResult.IsError.Should().BeFalse();
            createGymResult.Value.SubscriptionId.Should().Be(subscription.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task CreateGym_WhenCommandContainsInvalidData_ShouldReturnValidationError(int gymNameLength)
        {
            // Arrange
            string gymName = new('a', gymNameLength);
            var createGymCommand = GymCommandFactory.CreateCreateGymCommand(name: gymName);

            // Act
            var result = await _mediator.Send(createGymCommand);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be("Name");
        }
    }
}
