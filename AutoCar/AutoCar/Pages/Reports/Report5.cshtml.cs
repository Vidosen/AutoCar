using System.Data;
using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoCar.Pages.Reports;
public class Report5 : PageModel
{
    public record ReportData(string Brand, IEnumerable<MultiClientCarData> Cars);
    public ReportData Report { get; private set; }

    public void OnGet()
    {
    }
    public async Task OnPost([FromServices] IWebHostEnvironment appEnvironment, string brand)
    {
        await using var storage = new PostgresStorage();
        var multiClientCars = new List<MultiClientCarData>();
        if (string.IsNullOrEmpty(brand))
        {
            Report = new ReportData(brand, multiClientCars);
            return;
        }
        using var carsTable = await storage.ExecuteReportAsync(Path.Combine(appEnvironment.WebRootPath, "queries", "report5.sql"),
            new Dictionary<string, object> {{ "Brand", brand }});
        var clientQueryParams = new Dictionary<string, object>();
        foreach (DataRow multiClientCar in carsTable.Rows)
        {
            var carNumber = multiClientCar["CarNumber"].ToString();
            clientQueryParams["CarNumber"] = carNumber;
            using var clientsTable = await storage.ExecuteReportAsync(Path.Combine(appEnvironment.WebRootPath,
                    "queries", "report2_2.sql"), clientQueryParams);
            multiClientCars.Add(new MultiClientCarData(carNumber, ClientData.ExtractClientsDataFromTable(clientsTable)));
        }
        Report = new ReportData(brand, multiClientCars);
    }
}