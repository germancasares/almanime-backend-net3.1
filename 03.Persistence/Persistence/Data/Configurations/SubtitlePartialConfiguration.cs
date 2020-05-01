using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class SubtitlePartialConfiguration : IEntityTypeConfiguration<SubtitlePartial>
    {
        public void Configure(EntityTypeBuilder<SubtitlePartial> builder)
        {
            builder
                .HasKey(c => c.ID);

            builder
                .Property(c => c.ID)
                .ValueGeneratedOnAdd();

            builder
                .Property(c => c.CreationDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETUTCDATE()");

            builder
                .HasIndex(c => new { c.UserID, c.SubtitleID })
                .IsUnique();

            builder
                .HasOne(c => c.User)
                .WithMany(c => c.SubtitlePartials)
                .HasForeignKey(c => c.UserID);

            builder
                .HasOne(c => c.Subtitle)
                .WithMany(c => c.SubtitlePartials)
                .HasForeignKey(c => c.SubtitleID);
        }
    }
}
