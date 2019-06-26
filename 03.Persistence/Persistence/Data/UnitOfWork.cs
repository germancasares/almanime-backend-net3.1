using Persistence.Data.Repositories.Interfaces;
using System;

namespace Persistence.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AlmanimeContext _context;

        public UnitOfWork(
            AlmanimeContext context,
            IAnimeRepository animes,
            IUserRepository users,
            IFansubRepository fansubs,
            IMembershipRepository memberships,
            IImageRepository images
            )
        {
            _context = context;
            Animes = animes;
            Users = users;
            Fansubs = fansubs;
            Memberships = memberships;
            Images = images;
        }

        public IAnimeRepository Animes { get; }
        public IUserRepository Users { get; }
        public IFansubRepository Fansubs { get; }
        public IMembershipRepository Memberships { get; }

        public IImageRepository Images { get; }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save() => _context.SaveChanges();
    }
}
