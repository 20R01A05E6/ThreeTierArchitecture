using Microsoft.Extensions.DependencyInjection;
using YourNamespace.Data;
using YourNamespace.Repositories;
using YourNamespace.Services;

namespace YourNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationDbContext>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUserService, UserService>()
                .BuildServiceProvider();

            var userService = serviceProvider.GetService<IUserService>();

            // Start the web server
            var webServer = new WebServer(userService);
            webServer.Start();
        }
    }
}
