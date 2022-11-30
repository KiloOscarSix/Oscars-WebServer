using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oscars_WebApplication.Data;
using Oscars_WebApplication.Services;

namespace Oscars_WebApplication.Installers;

public class DbInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DataContext>(options =>
            options.UseSqlite(connectionString));

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<DataContext>();

        services.AddSingleton<IPostService, PostService>();
        services.AddSingleton<ILovenseService, LovenseService>();
    }
}