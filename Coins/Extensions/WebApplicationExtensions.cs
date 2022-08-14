

using Coins.Repository;
using Coins.Repository.Interfaces;
using Coins.UnitOfWork; 

namespace Coins.Extensions
{

    public static class WebApplicationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Load application configuration
            services.RegisterRepositories();
            services.RegisterUoW();
        }
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICoinRepository, CoinRepository>();
        }
        public static void RegisterUoW(this IServiceCollection services)
        {
            services.AddTransient<ICoinJar, CoinJar>();
        }
    }
}