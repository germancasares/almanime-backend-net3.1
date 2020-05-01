using Domain.Models;
using System;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByIdentityID(Guid id);
        User GetByName(string name);
    }
}
