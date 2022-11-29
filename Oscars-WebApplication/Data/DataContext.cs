using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Oscars_WebApplication.Data;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
}