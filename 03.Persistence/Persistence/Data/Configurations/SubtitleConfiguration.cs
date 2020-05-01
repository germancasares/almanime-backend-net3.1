using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class SubtitleConfiguration : BaseModelConfiguration<Subtitle>
    {
        public override void Configure(EntityTypeBuilder<Subtitle> builder)
        {
            base.Configure(builder);

            builder
                .HasIndex(c => new { c.EpisodeID, c.FansubID })
                .IsUnique();

            builder
                .HasOne(c => c.Episode)
                .WithMany(c => c.Subtitles)
                .HasForeignKey(c => c.EpisodeID);

            builder
                .HasOne(c => c.Fansub)
                .WithMany(c => c.Subtitles)
                .HasForeignKey(c => c.FansubID);
        }
    }
}
