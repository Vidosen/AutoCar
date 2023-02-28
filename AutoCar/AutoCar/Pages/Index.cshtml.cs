using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoCar.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Models.Client> Clients { get; private set; } = Enumerable.Empty<Models.Client>();

        public void OnGet(int listPage)
        {
            using (var storage = new PostgresStorage()) Clients = storage.Clients.ToArray();
        }
    }
}