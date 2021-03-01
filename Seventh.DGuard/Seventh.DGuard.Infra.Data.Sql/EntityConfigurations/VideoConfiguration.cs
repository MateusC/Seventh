using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventh.DGuard.Domain.Entities;

namespace Seventh.DGuard.Infra.Data.Sql.EntityConfigurations
{
    internal sealed class VideoConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder
                .Property(x => x.Path)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<bool>("IsDeleted");
        }
    }
}
