using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test
{
    public class DbTest : IDisposable
    {
        private string dbName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider ServiceProvider { get; private set; }

        public DbTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<AppDbContext>(x =>
                x.UseSqlServer(
                    $"Data Source=DESKTOP-8SL6PE8; initial catalog={dbName}; user id=sa; password=sa12345; Integrated Security=True"),
                    ServiceLifetime.Transient
                );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<AppDbContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<AppDbContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}