using AutoCar.Models;
using AutoCar.Services;
using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace AutoCar.Pages.Client;

public class NewClientModel : PageModel
{
    [BindProperty] public Models.Client Client { get; set; }
    [BindProperty] public string NewBirthDate { get; set; }
    public IEnumerable<string> ValidationMessages { get; private set; } = Enumerable.Empty<string>();
    public void OnGet()
    {
    }
    public IActionResult OnPost([FromServices] ValidationService service, [FromServices] PostgresStorage storage)
    {
        service.ValidateInitials(Client.FirstName, "Имя");
        service.ValidateInitials(Client.LastName, "Фамилия");
        service.ValidateInitials(Client.Patronymic, "Отчество");
        service.ValidatePhoneNumber(Client.PhoneNumber);
        service.ValidateAndRetrivedBirthDate(NewBirthDate,out var newBirthDate);
        Client.BirthDate = newBirthDate;
        if (service.PassedAllValidations)
        {
            storage.Clients.Add(Client);
            storage.SaveChanges();
        }
        ValidationMessages = service.GetValidationMessages();
        return service.PassedAllValidations ? RedirectToPage("../Index") : Page();
    }
    
}