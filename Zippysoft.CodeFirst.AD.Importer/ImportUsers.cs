using Bogus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using UUIDNext;
using Zippysoft.CodeFirst.DAL;
using Zippysoft.CodeFirst.DAL.Models;

namespace Zippysoft.CodeFirst.AD.Importer
{
    public class ImportUsers
    {
        private readonly BetterDbContext _context;
        
        public ImportUsers(BetterDbContext context)
        {
            _context = context;
        }

        [Function("ImportUsers")]
        public async Task Run([TimerTrigger("0 * * * * *", RunOnStartup = true)] TimerInfo timer, FunctionContext context)
        {
            var logger = context.GetLogger<ImportUsers>();
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            var testUsers = new Faker<Aduser>()
                //Ensure all properties have rules. By default, StrictMode is false
                //Set a global policy by using Faker.DefaultStrictMode
                .StrictMode(true)
                .RuleFor(o => o.Id, f => Uuid.NewDatabaseFriendly(UUIDNext.Database.SqlServer).ToString())
                .RuleFor(o => o.DisplayName, f => f.Name.FullName())
                .RuleFor(o => o.EmployeeHireDate, f => f.Date.Past(65, DateTime.Now).AddYears(-21))
                .RuleFor(o => o.PostalCode, f => f.Address.ZipCode())
                .RuleFor(o => o.State, f => f.Address.StateAbbr())
                .RuleFor(o => o.AlternateEmail, f=> f.Internet.Email())
                .RuleFor(o => o.PhoneNumber, f=> f.Phone.PhoneNumber());

            var tasks = new List<Task>();
            try
            {
                // This does not seem to save much time, but did before .NET Core
                this._context.ChangeTracker.AutoDetectChangesEnabled = false;
                for (short i = 0; i < 100; i++)
                {
                    tasks.Add(this._context.Users.AddRangeAsync(testUsers.Generate(50)));
                    logger.LogTrace("Task Count: {Tasks}, Completed: {Completed}", tasks.Count(),
                        tasks.Count(t => t.IsCompleted));
                    if (tasks.Count() > 5)
                    {
                        logger.LogInformation("Writing rows. Iterator: {iteration}", i);
                        await Task.WhenAll(tasks);
                        await this._context.SaveChangesAsync();
                        logger.LogInformation("Changes saved");
                        tasks.Clear();
                    }
                }

                await Task.WhenAll(tasks);
                await this._context.SaveChangesAsync();
            }
            finally
            {
                // Its important ot reset this.
                this._context.ChangeTracker.AutoDetectChangesEnabled = true;
            }

            logger.LogInformation($"Next timer schedule at: {timer.ScheduleStatus?.Next}");
        }
    }

    public class TimerInfo
    {
        public ScheduleStatus? ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class ScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
