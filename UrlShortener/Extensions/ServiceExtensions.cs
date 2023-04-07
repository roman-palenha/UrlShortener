using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Business;
using UrlShortener.Business.Interfaces;
using UrlShortener.Domain;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<IAboutMessageService, AboutMessageService>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.LoginPath = new PathString("/Home/Login");
                    o.AccessDeniedPath = new PathString("/Home/Login");
                });
        }

        public static void ConfigureSqlConnection(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(builder.Configuration.GetConnectionString(Constants.SqlConnection)));
        }
    }
}
