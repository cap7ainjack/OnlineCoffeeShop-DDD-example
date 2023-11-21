using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineCoffeeShop.Infrastructure.Identity.Database;
internal class IdentityApplicationDbContext: IdentityDbContext
{
    public IdentityApplicationDbContext(DbContextOptions<IdentityApplicationDbContext> options) : base(options)
    {

    }
}
