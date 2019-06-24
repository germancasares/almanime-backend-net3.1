using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder
                .HasKey(k => new { k.FansubID, k.UserID });

            builder
                .HasOne(c => c.Fansub)
                .WithMany(c => c.Memberships)
                .HasForeignKey(c => c.FansubID);

            builder
                .HasOne(c => c.User)
                .WithMany(c => c.Memberships)
                .HasForeignKey(c => c.UserID);
        }
    }
}
