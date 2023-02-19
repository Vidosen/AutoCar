using AutoCar.Models;
using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoCar.Pages;

public class IndexModel : PageModel
{
    public IEnumerable<Models.Client> Clients { get; private set; } = ArraySegment<Models.Client>.Empty;
    public void OnGet([FromServices] PostgresStorage storage)
    {
        Clients = storage.Clients.ToArray();
    }
}