using Microsoft.EntityFrameworkCore;
using Zippysoft.CodeFirst.DAL.Models;

namespace Zippysoft.CodeFirst.DAL;
public class BetterDbContext : DbContext
{
    public BetterDbContext(DbContextOptions<BetterDbContext> options) : base(options)
    {
    }

    public DbSet<Aduser>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aduser>()
            .HasIndex(user => user.State, "IX_Users_State_NY")
            .HasFilter("[State] = 'NY'")
            .IncludeProperties(nameof(Aduser.Id), nameof(Aduser.DisplayName));
    }

}