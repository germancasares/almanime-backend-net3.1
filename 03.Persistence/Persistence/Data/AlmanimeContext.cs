using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configurations;

namespace Persistence.Data
{
    public class AlmanimeContext : DbContext
    {
        public AlmanimeContext(DbContextOptions<AlmanimeContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnimeConfiguration());
            modelBuilder.ApplyConfiguration(new ChapterConfiguration());
            modelBuilder.ApplyConfiguration(new FansubConfiguration());
            modelBuilder.ApplyConfiguration(new MembershipConfiguration());
            modelBuilder.ApplyConfiguration(new SubtitleConfiguration());
            modelBuilder.ApplyConfiguration(new SubtitlePartialConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        #region DBSets

        public DbSet<Anime> Animes { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Fansub> Fansubs { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Subtitle> Subtitles { get; set; }
        public DbSet<SubtitlePartial> SubtitlePartials { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion
    }
}
