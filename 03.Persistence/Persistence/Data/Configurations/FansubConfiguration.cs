using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class FansubConfiguration : IEntityTypeConfiguration<Fansub>
    {
        public void Configure(EntityTypeBuilder<Fansub> builder)
        {
            // Will be implemented later on.
        }
    }
}
