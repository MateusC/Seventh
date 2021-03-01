using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventh.DGuard.Domain.Entities;

namespace Seventh.DGuard.Infra.Data.Sql.EntityConfigurations
{
    public class RecyclerConfiguration : IEntityTypeConfiguration<Recycler>
    {
        public void Configure(EntityTypeBuilder<Recycler> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.CreatedDate)
                .IsRequired();

            builder
                .Property(x => x.UpdatedDate)
                .IsRequired(false);

            builder
                .OwnsOne(x => x.Status, n =>
                {
                    n.Property(s => s.Id);
                    n.Property(s => s.Description);
                });
        }
    }
}
