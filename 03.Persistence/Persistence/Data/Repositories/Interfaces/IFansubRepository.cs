using System;
using Domain.Models;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IFansubRepository : IBaseRepository<Fansub>
    {
        void DeleteMembers(Guid fansubID);
    }
}
