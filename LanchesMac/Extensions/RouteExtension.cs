namespace LanchesMac.Extensions;

public static class RouteExtension
{
    public static void AddRoutes(this WebApplication app)
    {
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "teste",
            pattern: "testeme",
            defaults: new { Controller = "teste", Action = "index" });

        app.MapControllerRoute(
            name: "admin",
            pattern: "admin/{action=Index}/{id?}",
            defaults: new { Controller = "admin" });

        app.MapControllerRoute(
            name: "categoriaFiltro",
            pattern: "lanche/{action}/{categoria?}",
            defaults: new { Controller = "lanche", Action = "list" });

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}
