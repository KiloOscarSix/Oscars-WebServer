﻿using Microsoft.OpenApi.Models;

namespace Oscars_WebApplication.Installers;

public class MvcInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo {Title = "Oscar's API", Version = "v1"});
        });
    }
}