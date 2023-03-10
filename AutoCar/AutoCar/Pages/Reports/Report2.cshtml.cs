using System.Data;
using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoCar.Pages.Reports;

public class Report2 : PageModel
{
    public IEnumerable<MultiClientCarData> MultiClientCars { get; private set; }
    public async Task OnGet([FromServices] IWebHostEnvironment appEnvironment)
    {
        await using var storage = new PostgresStorage();
        var multiClientCars = new List<MultiClientCarData>();
        using var carsTable = await storage.ExecuteReportAsync(Path.Combine(appEnvironment.WebRootPath, "queries", "report2_1.sql"));
        var clientQueryParams = new Dictionary<string, object>();
        foreach (DataRow multiClientCar in carsTable.Rows)
        {
            var carNumber = multiClientCar["CarNumber"].ToString();
            clientQueryParams["CarNumber"] = carNumber;
            using var clientsTable = await storage.ExecuteReportAsync(Path.Combine(appEnvironment.WebRootPath,
                    "queries", "report2_2.sql"), clientQueryParams);
            multiClientCars.Add(new MultiClientCarData(carNumber, ClientData.ExtractClientsDataFromTable(clientsTable)));
        }
        MultiClientCars = multiClientCars;
    }
}