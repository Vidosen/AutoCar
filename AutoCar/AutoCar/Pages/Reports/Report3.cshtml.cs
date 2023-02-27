using AutoCar.Models;
using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoCar.Pages.Reports;
public class Report3 : PageModel
{
    public record ReportData(DateOnly FromDate, DateOnly ToDate, Car Car, decimal TotalDebt);

    public ReportData Report { get; private set; }

    public void OnGet()
    {
    }
    public async Task OnPost([FromServices] IWebHostEnvironment appEnvironment, DateTime startInterval, DateTime endInterval)
    {
        await using var storage = new PostgresStorage();
        using var table = await storage.ExecuteReportAsync(Path.Combine(appEnvironment.WebRootPath, "queries", "report3.sql"));
        var response = table.Rows[0];
        var fromDate = DateOnly.FromDateTime(startInterval);
        var toDate = DateOnly.FromDateTime(endInterval);
        var car = new Car()
        {
            Number = response["CarNumber"].ToString(),
            Brand = response["Brand"].ToString(),
            ReleaseYear = Convert.ToInt16(response["ReleaseYear"])
        };
        var totalDebt = Convert.ToDecimal(response["TotalDebt"]);
        Report = new ReportData(fromDate, toDate, car, totalDebt);
    }
}