using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventh.DGuard.Domain.Entities;

namespace Seventh.DGuard.Infra.Data.Sql.EntityConfigurations
{
    internal sealed class ServerConfiguration : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(x => x.IP)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(x => x.Port)
                .HasMaxLength(10)
                .IsRequired();

            builder
                .HasMany(x => x.Videos)
                .WithOne()
                .HasForeignKey("ServerId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property<bool>("IsDeleted");
        }
    }
}
