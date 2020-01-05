using Domain.Models;
using System;
using System.Linq;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IBaseRepository<TModel> where TModel : BaseModel
    {
        IQueryable<TModel> GetAll();
        TModel GetByID(Guid id);

        TModel Create(TModel entity);
        void Update(TModel entity);
        void Delete(Guid id);
        void Delete(TModel entity);
    }
}
