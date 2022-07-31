using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Zippysoft.CodeFirst.DAL;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
            .MinimumLevel.Override("Zippysoft.CodeFirst", LogEventLevel.Verbose)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
        services.AddLogging(lb =>
        {
            lb.ClearProviders(); //--> if used nothing works...
            lb.AddSerilog(Log.Logger, true);
        });

        services.AddDbContext<BetterDbContext>(optionsBuilder =>
        {
            optionsBuilder
                .EnableDetailedErrors()
                .EnableThreadSafetyChecks()
                .UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CodFirstBetter;Integrated Security=true;");
        });
    })
    .Build();

using (var scope = host.Services.CreateScope()) {
    var context = scope.ServiceProvider.GetRequiredService<BetterDbContext>();
    await context.Database.EnsureCreatedAsync();
    await context.Database.MigrateAsync();
}

host.Run();