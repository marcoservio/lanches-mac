using LanchesMac.Areas.Admin.Services;
using LanchesMac.Services;

namespace LanchesMac.Extensions;

public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        services.AddScoped<RelatorioVendasService>();
        services.AddScoped<GraficoVendasService>();
        services.AddScoped<RelatorioLanchesService>();
    }
}
