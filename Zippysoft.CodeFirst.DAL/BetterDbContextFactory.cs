using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Zippysoft.CodeFirst.DAL;

/// <summary>
/// Used by <c>dotnet ef</c> commands.
/// </summary>
/// <remarks>
/// This fixture accomplishes two things. Firstly, it prevents the needs to set <c>--startup-project</c>.
/// Secondly, we can set options here for migrations such as timeout that we don't want for the regular app.</remarks>
public partial class BetterDbContextFactory : IDesignTimeDbContextFactory<BetterDbContext>
{
    /// <inheritdoc />
    public BetterDbContext CreateDbContext(string[] args)
    {
        Console.WriteLine("==============  Inside BetterDbContextFactory  ==============");

        var connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CodeFirstBetter;Integrated Security=true;";
        var optionsBuilder = new DbContextOptionsBuilder<BetterDbContext>();
        
        optionsBuilder.UseSqlServer(connectionString, opts =>
        {
            // Set the timeout to 5 minutes
            opts.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
        });

        return new BetterDbContext(optionsBuilder.Options);
    }
}