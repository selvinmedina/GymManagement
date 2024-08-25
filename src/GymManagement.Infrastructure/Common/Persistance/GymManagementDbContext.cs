using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymManagement.Infrastructure.Common.Persistance
{
    internal class GymManagementDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        public DbSet<Gym> Gyms { get; set; } = null!;

        public GymManagementDbContext(DbContextOptions<GymManagementDbContext> options) : base(options)
        {
        }

        public async Task CommitChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
