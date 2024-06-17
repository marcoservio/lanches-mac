using LanchesMac.Enums;

using Microsoft.AspNetCore.Identity;

namespace LanchesMac.Services;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync(nameof(Roles.Member)).Result)
        {
            _ = _roleManager.CreateAsync(new IdentityRole()
            {
                Name = nameof(Roles.Member),
                NormalizedName = nameof(Roles.Member).ToUpper()
            }).Result;
        }

        if (!_roleManager.RoleExistsAsync(nameof(Roles.Admin)).Result)
        {
            _ = _roleManager.CreateAsync(new IdentityRole()
            {
                Name = nameof(Roles.Admin),
                NormalizedName = nameof(Roles.Admin).ToUpper()
            }).Result;
        }
    }

    public void SeedUsers()
    {
        if(_userManager.FindByEmailAsync("marco@gmail.com").Result == null)
        {
            var user = new IdentityUser
            {
                UserName = "marco@gmail.com",
                Email = "marco@gmail.com",
                NormalizedUserName = "MARCO@GMAIL.COM",
                NormalizedEmail = "MARCO@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = _userManager.CreateAsync(user, "123456").Result;

            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, "Member").Wait();
        }

        if (_userManager.FindByEmailAsync("admin@gmail.com").Result == null)
        {
            var user = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = _userManager.CreateAsync(user, "123456").Result;

            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, "Admin").Wait();
        }
    }
}
