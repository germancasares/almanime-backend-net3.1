using Domain.Models;
using Persistence.Data.Repositories.Interfaces;
using System;
using System.Linq;

namespace Persistence.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AlmanimeContext context) : base(context) { }

        public User GetByIdentityID(Guid id) => GetAll().SingleOrDefault(p => p.IdentityID == id);

        public User GetByNickName(string name) => GetAll().SingleOrDefault(u => u.NickName == name);
    }
}
