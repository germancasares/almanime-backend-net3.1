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
                .HasOne(c => c.Chapter)
                .WithMany(c => c.Subtitles)
                .HasForeignKey(c => c.ChapterID);
        }
    }
}
