using AutoCar.Services;
using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoCar.Pages.Clients;

public class NewClientModel : PageModel
{
    [BindProperty] public Models.Client Client { get; set; }
    [BindProperty] public string NewBirthDate { get; set; }
    public IEnumerable<string> ValidationMessages { get; private set; } = Enumerable.Empty<string>();
    public void OnGet()
    {
    }
    public IActionResult OnPost([FromServices] ValidationService service)
    {
        service.ValidateInitials(Client.FirstName, "Имя");
        service.ValidateInitials(Client.LastName, "Фамилия");
        service.ValidateInitials(Client.Patronymic, "Отчество");
        service.ValidatePhoneNumber(Client.PhoneNumber);
        service.ValidateAndRetrieveBirthDate(NewBirthDate,out var newBirthDate);
        Client.BirthDate = newBirthDate;
        if (service.PassedAllValidations)
        {
            using (var storage = new PostgresStorage())
            {
                storage.Clients.Add(Client);
                storage.SaveChanges();
            }
        }
        ValidationMessages = service.GetValidationMessages();
        return service.PassedAllValidations ? RedirectToPage("/Index") : Page();
    }
    
}