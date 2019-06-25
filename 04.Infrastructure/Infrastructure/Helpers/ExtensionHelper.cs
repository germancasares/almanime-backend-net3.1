using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.Helpers
{
    public static class ExtensionHelper
    {
        public static Guid GetIdentityID(this IEnumerable<Claim> claims)
        {
            var id = claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            return id == null ? Guid.Empty : new Guid(id);
        }
    }
}
