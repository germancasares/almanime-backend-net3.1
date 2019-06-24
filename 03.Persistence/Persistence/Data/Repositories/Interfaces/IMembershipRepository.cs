using Domain.Models;
using System;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IMembershipRepository
    {
        Membership Create(Membership member);
        bool IsFounder(Guid fansubID, Guid userTrigger);
    }
}
