using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class EpisodeConfiguration : BaseModelConfiguration<Episode>
    {
        public override void Configure(EntityTypeBuilder<Episode> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(c => c.Anime)
                .WithMany(c => c.Episodes)
                .HasForeignKey(c => c.AnimeID);
        }
    }
}
