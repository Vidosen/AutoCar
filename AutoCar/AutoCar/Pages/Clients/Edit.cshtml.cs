using AutoCar.Services;
using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace AutoCar.Pages.Clients;

public class EditClientModel : PageModel
{
    private PostgresStorage _storage;
    public EditClientModel(PostgresStorage storage)
    {
        _storage = storage;
    }
    [BindProperty] public Models.Client Client { get; set; }
    [BindProperty] public string NewBirthDate { get; set; }
    public IEnumerable<string> ValidationMessages { get; private set; } = Enumerable.Empty<string>();
    public IActionResult OnGet(int id)
    {
        var foundClient = _storage.Clients.Find(id);
        Client = foundClient;
        return foundClient != null ? Page() : NotFound();
    }
    public IActionResult OnPost([FromServices] ValidationService service)
    {
        service.ValidateInitials(Client.FirstName, "Имя");
        service.ValidateInitials(Client.LastName, "Фамилия");
        service.ValidateInitials(Client.Patronymic, "Отчество");
        service.ValidatePhoneNumber(Client.PhoneNumber);
        service.ValidateAndRetrieveBirthDate(NewBirthDate,out var newBirthDate);
        Client.BirthDate = newBirthDate;
        if (TryGetClientForEdit(out var dbClient) && service.PassedAllValidations)
        {
            dbClient.FirstName = Client.FirstName;
            dbClient.LastName = Client.LastName;
            dbClient.Patronymic = Client.Patronymic;
            dbClient.PhoneNumber = Client.PhoneNumber;
            dbClient.PhoneNumber = Client.PhoneNumber;
            dbClient.BirthDate = Client.BirthDate;
            _storage.SaveChanges();
        }
        ValidationMessages = service.GetValidationMessages();
        return service.PassedAllValidations ? RedirectToPage("../Index") : Page();
    }

    private bool TryGetClientForEdit(out Models.Client dbClient)
    {
        dbClient = null;
        return Client != null && (dbClient = _storage.Clients.Find(Client.Id)) != null;
    }
}