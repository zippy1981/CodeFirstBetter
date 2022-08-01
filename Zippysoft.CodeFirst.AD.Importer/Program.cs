using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Zippysoft.CodeFirst.DAL;

var host = new HostBuilder()
    .ConfigureServices(services =>
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Verbose)
            .MinimumLevel.Override("Zippysoft.CodeFirst", LogEventLevel.Verbose)
            .Enrich.FromLogContext()
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} - {SourceContext}] {Message:lj}{NewLine}{Exception}")
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
                    "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CodeFirstBetter;Integrated Security=true;");
        });
    })
    .ConfigureFunctionsWorkerDefaults()
    .Build();

using (var scope = host.Services.CreateScope()) {
    var context = scope.ServiceProvider.GetRequiredService<BetterDbContext>();
    await context.Database.EnsureCreatedAsync();
    await context.Database.MigrateAsync();
}

host.Run();