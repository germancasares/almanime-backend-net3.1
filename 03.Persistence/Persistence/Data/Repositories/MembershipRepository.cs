using System;
using System.Linq;
using Domain.Enums;
using Domain.Models;
using Persistence.Data.Repositories.Interfaces;

namespace Persistence.Data.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly AlmanimeContext _context;

        public MembershipRepository(AlmanimeContext context)
        {
            _context = context;
        }

        public Membership Create(Membership member) => _context.Add(member).Entity;

        public bool IsFounder(Guid fansubID, Guid userTrigger) => _context.Set<Membership>().Any(m => m.FansubID == fansubID && m.UserID == userTrigger && m.Role == EFansubRole.Founder);
    }
}
