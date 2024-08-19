using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Common.Persistance
{
    internal class GymManagementDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Subscription> Subscriptions { get; set; } = null!;

        public GymManagementDbContext(DbContextOptions<GymManagementDbContext> options) : base(options)
        {
        }

        public async Task CommitChangesAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}
