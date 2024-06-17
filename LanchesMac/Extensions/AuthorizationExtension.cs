using LanchesMac.Enums;

namespace LanchesMac.Extensions;

public static class AuthorizationExtension
{
    public static void AddPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(nameof(Roles.Admin), policy =>
            {
                policy.RequireRole(nameof(Roles.Admin));
            });
        });
    }
}
