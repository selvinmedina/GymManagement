﻿using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Gyms.Persistance
{
    internal class GymsRepository(GymManagementDbContext dbContext) : IGymsRepository
    {
        private readonly GymManagementDbContext _dbContext = dbContext;
        public async Task AddGymAsync(Gym gym)
        {
            await _dbContext.Gyms.AddAsync(gym);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbContext.Gyms.AsNoTracking().AnyAsync(gym => gym.Id == id);
        }

        public async Task<Gym?> GetGymByIdAsync(Guid id)
        {
            return await _dbContext.Gyms.FirstOrDefaultAsync(gym => gym.Id == id);
        }

        public async Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await _dbContext.Gyms
                .Where(gym => gym.SubscriptionId == subscriptionId)
                .ToListAsync();
        }

        public Task RemoveGymAsync(Gym gym)
        {
            _dbContext.Gyms.Remove(gym);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(List<Gym> gyms)
        {
            _dbContext.Gyms.RemoveRange(gyms);
            return Task.CompletedTask;
        }

        public Task UpdateGymAsync(Gym gym)
        {
            _dbContext.Gyms.Update(gym);
            return Task.CompletedTask;
        }
    }
}
