using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ShoppingApi.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost MigrateDatabase<TContext>(this IWebHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<TContext>>();
                var dbContext = services.GetService<TContext>();

                try
                {
                    logger.LogInformation($"Starting migrating database associated with {typeof(TContext).Name} context");
                    dbContext.Database.Migrate();
                    logger.LogInformation($"Finished migrating database associated with {typeof(TContext).Name} context");
                }
                catch (Exception e)
                {
                    logger.LogError(e, $"An error occured while migrating the database on {typeof(TContext).Name} context");
                }
            }

            return host;
        }
    }
}
