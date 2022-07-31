using Microsoft.EntityFrameworkCore;
using Zippysoft.CodeFirst.DAL.Models;

namespace Zippysoft.CodeFirst.DAL;
public class BetterDbContext : DbContext
{
    public BetterDbContext(DbContextOptions<BetterDbContext> options) : base(options)
    {
    }

    public DbSet<Aduser>? Users { get; set; }
}
