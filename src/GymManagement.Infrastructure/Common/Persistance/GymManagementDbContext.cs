using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Domain.Common;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymManagement.Infrastructure.Common.Persistance
{
    public class GymManagementDbContext(
        DbContextOptions<GymManagementDbContext> options,
        IHttpContextAccessor httpContextAccessor) 
        : DbContext(options), IUnitOfWork
    {

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        public DbSet<Gym> Gyms { get; set; } = null!;

        public async Task CommitChangesAsync()
        {
            // get hold of all the domain events
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .SelectMany(x => x.Entity.PopDomainEvents())
                .ToList();

            // store them in the http context for later
            AddDomainEventsToOfflineProcessingQueue(domainEvents);

            await base.SaveChangesAsync();
        }

        private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
        {
            // fetch queue from http context or create a new one if it doesn't exist
            var domainEventsQueue = _httpContextAccessor.HttpContext!.Items
                                    .TryGetValue("DomainEventsQueue", out var value) 
                                    && value is Queue<IDomainEvent> existingDomainEvent 
                                    ? existingDomainEvent 
                                    : new Queue<IDomainEvent>();

            // add the domain events to the end of the queue
            domainEvents.ForEach(domainEventsQueue.Enqueue);

            // store the queue back in the http context
            _httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
