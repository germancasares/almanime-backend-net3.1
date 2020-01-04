using System;
using System.Linq;
using Domain.Enums;
using Domain.Models;
using Persistence.Data.Repositories.Interfaces;

namespace Persistence.Data.Repositories
{
    public class MembershipRepository : BaseRepository<Membership>, IMembershipRepository
    {
        public MembershipRepository(AlmanimeContext context) : base(context) { }

        public bool IsFounder(Guid fansubID, Guid userTrigger) => GetAll().Any(m => m.FansubID == fansubID && m.UserID == userTrigger && m.Role == EFansubRole.Founder);
    }
}
