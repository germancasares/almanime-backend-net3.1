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
        }

        #region DBSets

        public DbSet<Anime> Animes { get; set; }
        public DbSet<User> Users { get; set; }


        #endregion
    }
}
