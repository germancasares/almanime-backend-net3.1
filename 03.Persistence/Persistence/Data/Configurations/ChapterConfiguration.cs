using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class ChapterConfiguration : BaseModelConfiguration<Chapter>
    {
        public override void Configure(EntityTypeBuilder<Chapter> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(c => c.Anime)
                .WithMany(c => c.Chapters)
                .HasForeignKey(c => c.AnimeID);
        }
    }
}
