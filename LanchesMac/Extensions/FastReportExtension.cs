using FastReport.Data;
using FastReport.Utils;

namespace LanchesMac.Extensions;

public static class FastReportExtension
{
    public static void AddFastReports(this IServiceCollection services)
    {
        RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));

        services.AddFastReport();
    }
}
