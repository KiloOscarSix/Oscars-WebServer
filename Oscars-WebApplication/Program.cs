using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Oscars_WebApplication.Configuration;
using Oscars_WebApplication.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo {Title = "Oscar's API", Version = "v1"});
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

SwaggerOptions swaggerOptions = new();
builder.Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

app.UseSwagger(options =>
{
    options.RouteTemplate = swaggerOptions.JsonRoute;
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();