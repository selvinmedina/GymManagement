﻿using ErrorOr;
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
    }
}
