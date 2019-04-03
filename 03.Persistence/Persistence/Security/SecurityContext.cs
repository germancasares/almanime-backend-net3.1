using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Security.Core
{
    public class SecurityContext : IdentityDbContext
    {
        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options) { }
    }
}
