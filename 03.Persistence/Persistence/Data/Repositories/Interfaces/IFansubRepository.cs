using System;
using System.Collections.Generic;
using Domain.Models;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IFansubRepository : IBaseRepository<Fansub>
    {
        void DeleteMembers(Guid fansubID);
        Fansub GetByAcronym(string acronym);
        Fansub GetByFullName(string fullname);
    }
}
