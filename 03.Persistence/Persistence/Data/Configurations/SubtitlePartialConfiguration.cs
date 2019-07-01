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
                .HasKey(c => new { c.MembershipID, c.SubtitleID });

            builder
                .Property(c => c.RevisionDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETUTCDATE()");

            builder
                .HasOne(c => c.Membership)
                .WithMany(c => c.SubtitlePartials)
                .HasForeignKey(c => c.MembershipID);

            builder
                .HasOne(c => c.Subtitle)
                .WithMany(c => c.SubtitlePartials)
                .HasForeignKey(c => c.SubtitleID);
        }
    }
}
