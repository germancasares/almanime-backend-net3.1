using Domain.Models;
using System;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface ISubtitlePartialRepository
    {
        SubtitlePartial Create(SubtitlePartial entity);
        void Delete(Guid id);
        void Delete(SubtitlePartial subtitlePartial);
        SubtitlePartial GetByID(Guid id);
    }
}
