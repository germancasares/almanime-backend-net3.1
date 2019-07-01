using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class MembershipConfiguration : BaseModelConfiguration<Membership>
    {
        public override void Configure(EntityTypeBuilder<Membership> builder)
        {
            base.Configure(builder);

            builder
                .HasIndex(k => new { k.FansubID, k.UserID })
                .IsUnique();

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
