using AutoCar.Models;
using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoCar.Pages;

public class IndexModel : PageModel
{
    public IEnumerable<ClientModel> Clients { get; private set; } = ArraySegment<ClientModel>.Empty;
    public void OnGet([FromServices] PostgresStorage storage)
    {
        Clients = storage.Clients.ToArray();
    }
}