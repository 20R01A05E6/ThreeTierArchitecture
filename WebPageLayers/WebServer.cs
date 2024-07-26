using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using YourNamespace.Models;
using YourNamespace.Services;

namespace YourNamespace
{
    public class WebServer
    {
        private readonly IUserService _userService;

        public WebServer(IUserService userService)
        {
            _userService = userService;
        }

        public void Start()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot("wwwroot")
                .UseUrls("http://localhost:5001") // Change port here
                .ConfigureServices(services => services.AddSingleton(_userService))
                .Configure(app =>
                {
                    app.Run(async (context) =>
                    {
                        if (context.Request.Path == "/login" && context.Request.Method == "POST")
                        {
                            await HandleLogin(context);
                        }
                        else if (context.Request.Path == "/register" && context.Request.Method == "POST")
                        {
                            await HandleRegister(context);
                        }
                        else if (context.Request.Path == "/login" || context.Request.Path == "/register")
                        {
                            var filePath = context.Request.Path == "/login" ? "C:\\Users\\Purandhar Kola\\source\\repos\\WebPageLayers\\WebPageLayers\\wwwroot\\login.html" : "C:\\Users\\Purandhar Kola\\source\\repos\\WebPageLayers\\WebPageLayers\\wwwroot\\register.html";
                            context.Response.ContentType = "text/html";
                            await context.Response.SendFileAsync(filePath);
                        }
                        else
                        {
                            context.Response.StatusCode = 404;
                            await context.Response.WriteAsync("Not Found");
                        }
                    });
                })
                .Build();

            host.Run();
        }

        private async Task HandleLogin(HttpContext context)
        {
            var form = context.Request.Form;
            var username = form["username"];
            var password = form["password"];

            if (_userService.Login(username, password))
            {
                await context.Response.WriteAsync("Login successful!");
            }
            else
            {
                await context.Response.WriteAsync("Invalid username or password.");
            }
        }

        private async Task HandleRegister(HttpContext context)
        {
            var form = context.Request.Form;
            var username = form["username"];
            var password = form["password"];
            var email = form["email"];

            var user = new User { Username = username, Password = password, Email = email };
            _userService.Register(user);

            await context.Response.WriteAsync("Registration successful!");
        }
    }
}
