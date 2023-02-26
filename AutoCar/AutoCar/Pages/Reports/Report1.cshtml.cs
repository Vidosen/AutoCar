using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoCar.Pages.Reports;

public class Report1 : PageModel
{
    public record ReportData(int Id, string FullName, decimal TotalDebt, DateTime? LastPaymentDate);
    public ReportData Report { get; private set; }
    public async Task OnGet([FromServices] IWebHostEnvironment appEnvironment)
    {
        await using var storage = new PostgresStorage();
        using var table = await storage.ExecuteReportAsync(Path.Combine(appEnvironment.WebRootPath, "queries", "report1.sql"));
        var response = table.Rows[0];
        
        var id = Convert.ToInt32(response["Id"]);
        var fullName = response["FullName"].ToString();
        var totalDebt = Convert.ToDecimal(response["TotalDebt"]);
        DateTime? lastPaymentDate = response["LastPaymentDate"] is not DBNull
            ? Convert.ToDateTime(response["LastPaymentDate"])
            : null;
        
        Report = new ReportData(id, fullName, totalDebt, lastPaymentDate);
    }

}