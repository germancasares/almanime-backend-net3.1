using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Repositories.Interfaces;
using System;
using System.Linq;

namespace Persistence.Data.Repositories
{
    public class SubtitlePartialRepository : ISubtitlePartialRepository
    {
        private readonly DbSet<SubtitlePartial> _dbSet;

        public SubtitlePartialRepository(AlmanimeContext context) => _dbSet = context.Set<SubtitlePartial>();

        public SubtitlePartial GetByID(Guid id) => _dbSet.SingleOrDefault(o => o.ID == id);
        public SubtitlePartial Create(SubtitlePartial entity) => _dbSet.Add(entity).Entity;
        public void Delete(Guid id) => Delete(GetByID(id));
        public void Delete(SubtitlePartial subtitlePartial) => _dbSet.Remove(subtitlePartial);
    }
}
