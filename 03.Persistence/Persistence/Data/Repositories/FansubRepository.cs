using System;
using System.Linq;
using Domain.Models;
using Persistence.Data.Repositories.Interfaces;

namespace Persistence.Data.Repositories
{
    public class FansubRepository : BaseRepository<Fansub>, IFansubRepository
    {
        public FansubRepository(AlmanimeContext context) : base(context) { }

        public void DeleteMembers(Guid fansubID) => GetByID(fansubID).Memberships.Clear();

        public Fansub GetByFullName(string fullname) => GetAll().SingleOrDefault(f => f.FullName == fullname);
        public Fansub GetByAcronym(string acronym) => GetAll().SingleOrDefault(f => f.Acronym == acronym);
    }
}
