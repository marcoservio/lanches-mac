using LanchesMac.Services;

namespace LanchesMac.Extensions;

public static class SeedExtension
{
    public static void SeedIdentity(this WebApplication app)
    {
        var seed = app.Services.CreateScope().ServiceProvider.GetService<ISeedUserRoleInitial>();

        seed.SeedRoles();
        seed.SeedUsers();
    }
}
