using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Data;

public class AppIdentityDbContext:IdentityDbContext<IdentityUser>
{
    public AppIdentityDbContext(DbContextOptions options) : base(options)
    { }
}
