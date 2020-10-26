using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class AnimeConfiguration : BaseModelConfiguration<Anime>
    {
        public override void Configure(EntityTypeBuilder<Anime> builder)
        {
            base.Configure(builder);

            builder
                .HasIndex(a => a.KitsuID)
                .IsUnique();

            builder.Property(a => a.KitsuID).IsRequired();
            builder.Property(a => a.Slug).IsRequired();
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.Season).IsRequired();
            builder.Property(a => a.Status).IsRequired();
            builder.Property(a => a.StartDate).IsRequired();
        }
    }
}
