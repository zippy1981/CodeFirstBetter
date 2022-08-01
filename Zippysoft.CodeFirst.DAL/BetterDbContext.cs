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
            .HasIndex(user => user.AlternateEmail, "IX_Users_Email_YahooCom")
            // No LIkE clauses on filtered indexes
            // .HasFilter("[AlternateEmail] LIKE '%@yahoo.com'")
            .IncludeProperties(nameof(Aduser.Id), nameof(Aduser.DisplayName));
    }

}