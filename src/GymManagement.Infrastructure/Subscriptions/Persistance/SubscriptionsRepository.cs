using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Subscriptions.Persistance
{
    internal class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly GymManagementDbContext _dbContext;

        public SubscriptionsRepository(GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            await _dbContext.Subscriptions.AddAsync(subscription);
        }

        public async Task<Subscription?> GetByIdAsync(Guid subscriptionId)
        {
            var subscription = await _dbContext.Subscriptions.FindAsync(subscriptionId);

            return subscription;
        }
    }
}
