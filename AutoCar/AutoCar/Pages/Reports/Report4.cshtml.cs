using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoCar.Pages.Reports;
public class Report4 : PageModel
{
    public record ReportData(DateOnly FromDate, DateOnly ToDate, decimal TotalDebt);

    public ReportData Report { get; private set; }

    public void OnGet()
    {
    }
    public async Task OnPost([FromServices] IWebHostEnvironment appEnvironment, DateTime startInterval, DateTime endInterval)
    {
        await using var storage = new PostgresStorage();
        var fromDate = DateOnly.FromDateTime(startInterval);
        var toDate = DateOnly.FromDateTime(endInterval);
        using var table = await storage.ExecuteReportAsync(Path.Combine(appEnvironment.WebRootPath, "queries", "report4.sql"),
            new Dictionary<string, object>()
            {
                { "StartInterval", fromDate },
                { "EndInterval", toDate }
            });
        var response = table.Rows[0];
        var totalDebt = response["TotalDebt"] is DBNull ? 0 : Convert.ToDecimal(response["TotalDebt"]);
        Report = new ReportData(fromDate, toDate, totalDebt);
    }
}