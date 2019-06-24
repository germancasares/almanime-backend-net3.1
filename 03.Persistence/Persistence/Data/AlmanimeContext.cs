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
            modelBuilder.ApplyConfiguration(new MembershipConfiguration());
            modelBuilder.ApplyConfiguration(new FansubConfiguration());
        }

        #region DBSets

        public DbSet<Anime> Animes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Fansub> Fansubs { get; set; }


        #endregion
    }
}
