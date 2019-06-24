using System;
using Domain.Models;
using Persistence.Data.Repositories.Interfaces;

namespace Persistence.Data.Repositories
{
    public class FansubRepository : BaseRepository<Fansub>, IFansubRepository
    {
        public FansubRepository(AlmanimeContext context) : base(context) { }

        public void DeleteMembers(Guid fansubID) => GetByID(fansubID).Memberships.Clear();
    }
}
