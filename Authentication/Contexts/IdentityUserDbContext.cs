using Authentication.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Contexts
{
    public class IdentityUserDbContext(DbContextOptions<IdentityUserDbContext> options) : IdentityDbContext<AppUser>(options)
    {

    }
}
