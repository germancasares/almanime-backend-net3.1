using Domain.Models;
using Persistence.Data.Repositories.Interfaces;
using System;

namespace Persistence.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        IAnimeRepository Animes { get; }
        IBaseRepository<Episode> Episodes { get; }
        IFansubRepository Fansubs { get; }
        IMembershipRepository Memberships { get; }
        IStorageRepository Storage { get; }
        IBaseRepository<Subtitle> Subtitles { get; set; }
        ISubtitlePartialRepository SubtitlePartials { get; set; }
        IUserRepository Users { get; }
    }
}
