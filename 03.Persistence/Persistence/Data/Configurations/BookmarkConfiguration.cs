using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class BookmarkConfiguration : BaseModelConfiguration<Bookmark>
    {
        public override void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            base.Configure(builder);

            builder
                .HasIndex(c => new { c.AnimeID, c.UserID })
                .IsUnique();

            builder
                .HasOne(c => c.Anime)
                .WithMany(c => c.Bookmarks)
                .HasForeignKey(c => c.AnimeID);

            builder
                .HasOne(c => c.User)
                .WithMany(c => c.Bookmarks)
                .HasForeignKey(c => c.UserID);
        }
    }
}
