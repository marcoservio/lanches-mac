using LanchesMac.Context;

using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Extensions;

public static class DatabaseExtension
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}
