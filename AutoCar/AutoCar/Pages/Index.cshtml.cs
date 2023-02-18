using AutoCar.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoCar.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private List<ClientModel> _clients;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        _clients = new List<ClientModel>();
    }

    public IReadOnlyList<ClientModel> Clients => _clients;

    public void OnGet()
    {
        _clients.Clear();
        _clients.Add(new ClientModel()
        {
            Id = _clients.Count,
            FirstName = "Иванов",
            LastName = "Иван",
            Patronymic = "Иванович",
            BirthDate = new DateOnly(1993, 4, 20),
            PhoneNumber = "8935123456"
        });
        _clients.Add(new ClientModel()
        {
            Id = _clients.Count,
            FirstName = "Петров",
            LastName = "Алексей",
            Patronymic = "Петрович",
            BirthDate = new DateOnly(1986, 4, 23),
            PhoneNumber = "8241459673"
        });
    }
}