using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Gyms.Persistance
{
    public class GymConfigurations : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .ValueGeneratedNever();

            builder.Property("_maxRooms")
                .HasColumnName("MaxRooms");

            builder.Property<List<Guid>>("_roomIds")
                .HasColumnName("RoomIds")
                .HasListOfIdsCoverter();

            builder.Property<List<Guid>>("_trainerIds")
                .HasColumnName("TrainerIds")
                .HasListOfIdsCoverter();

            builder.Property(g => g.Name);

            builder.Property(g => g.SubscriptionId);
        }
    }
}
