using Persistence.Data.Repositories.Interfaces;
using System;

namespace Persistence.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        IAnimeRepository Animes { get; }
        IUserRepository Users { get; }
        IFansubRepository Fansubs { get; }
        IMembershipRepository Memberships { get; }
        IImageRepository Images { get; }
    }
}
