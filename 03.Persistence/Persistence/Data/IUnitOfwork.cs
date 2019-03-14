using Persistence.Data.Repositories.Interfaces;
using System;

namespace Persistence.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        IAnimeRepository Animes { get; }
    }
}
