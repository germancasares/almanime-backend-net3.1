using Domain.Models;
using System;
using System.Linq;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IQueryable<User> GetByFansub(string acronym);
        User GetByIdentityID(Guid id);
        User GetByName(string name);
    }
}
