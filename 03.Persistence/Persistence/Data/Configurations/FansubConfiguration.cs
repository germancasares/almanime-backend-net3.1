using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class FansubConfiguration : BaseModelConfiguration<Fansub>
    {
        public override void Configure(EntityTypeBuilder<Fansub> builder)
        {
            base.Configure(builder);

            builder
                .HasIndex(f => f.FullName)
                .IsUnique();

            builder
                .HasIndex(f => f.Acronym)
                .IsUnique();

            builder.Property(f => f.FullName).IsRequired();
            builder.Property(f => f.CreationDate).IsRequired();
            builder.Property(f => f.MainLanguage).IsRequired();
            builder.Property(f => f.MembershipOption).IsRequired();
        }
    }
}
