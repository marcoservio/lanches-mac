using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.Repositories;

namespace LanchesMac.Extensions;

public static class RepositoriesExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ILancheRepository, LancheRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped(sp => CarrinhoCompra.Get(sp));
    }
}
