using LanchesMac.Extensions;
using LanchesMac.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddContext(builder.Configuration);

builder.Services.AddFastReports();

builder.Services.AddIdentity();

builder.Services.Configure<ConfigurationImagens>(builder.Configuration.GetSection("ConfigurationPastaImagens"));

builder.Services.AddRepositories();

builder.Services.AddServices();

builder.Services.AddPolicies();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllersWithViews();

builder.Services.AddPagination();

builder.Services.AddMemoryCache();

builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseFastReport();

app.UseRouting();

app.SeedIdentity();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.AddRoutes();

app.Run();