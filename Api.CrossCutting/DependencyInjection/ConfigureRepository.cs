using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer("Data Source=DESKTOP-8SL6PE8; initial catalog=DddDb; user id=sa; password=sa12345; Integrated Security=True")
            );
        }
    }
}