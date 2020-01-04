using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Repositories.Interfaces;
using System;
using System.Linq;

namespace Persistence.Data.Repositories
{
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : BaseModel
    {
        private readonly AlmanimeContext _context;

        public BaseRepository(AlmanimeContext context) => _context = context;

        public IQueryable<TModel> GetAll() => _context.Set<TModel>().AsQueryable();
        public TModel GetByID(Guid id) => _context.Set<TModel>().SingleOrDefault(o => o.ID == id);

        public TModel Create(TModel entity) => _context.Set<TModel>().Add(entity).Entity;

        public void Update(TModel entity)
        {
            entity.ModificationDate = DateTime.UtcNow;

            _context.Set<TModel>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public TModel Delete(Guid id)
        {
            var entity = GetByID(id);

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TModel>().Attach(entity);
            }
            return _context.Set<TModel>().Remove(entity).Entity;
        }

        public TModel Delete(TModel entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TModel>().Attach(entity);
            }
            return _context.Set<TModel>().Remove(entity).Entity;
        }
    }
}
