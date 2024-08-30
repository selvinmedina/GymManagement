using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Admins.Persistance
{
    internal class AdminsRepository(GymManagementDbContext dbContext) : IAdminsRepository
    {
        private readonly GymManagementDbContext _dbContext = dbContext;

        public Task<Admin?> GetByIdAsync(Guid adminId)
        {
            return _dbContext.Admins.FirstOrDefaultAsync(a => a.Id == adminId);
        }

        public Task UpdateAsync(Admin admin)
        {
            _dbContext.Admins.Update(admin);

            return Task.CompletedTask;
        }
    }
}
