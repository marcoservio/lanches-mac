using ReflectionIT.Mvc.Paging;

namespace LanchesMac.Extensions;

public static class PaginationExtension
{
    public static void AddPagination(this IServiceCollection services)
    {
        services.AddPaging(options =>
        {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageindex";
        });
    }
}
