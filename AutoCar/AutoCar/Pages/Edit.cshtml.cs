using AutoCar.Models;
using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace AutoCar.Pages;

public class EditModel : PageModel
{
    private PostgresStorage _storage;
    public EditModel(PostgresStorage storage)
    {
        _storage = storage;
    }
    [BindProperty] public ClientModel? Client { get; set; }
    [BindProperty] public string? NewBirthDate { get; set; }
    public IActionResult OnGet(int id)
    {
        var foundClient = _storage.Clients.Find(id);
        Client = foundClient;
        return foundClient != null ? Page() : NotFound();
    }
    public IActionResult OnPost()
    {
        ClientModel? dbClient;
        if (Client == null || (dbClient = _storage.Clients.Find(Client.Id)) == null)
        {
            return RedirectToPage("Error");
        }
        dbClient.FirstName = Client.FirstName;
        dbClient.LastName = Client.LastName;
        dbClient.Patronymic = Client.Patronymic;
        dbClient.PhoneNumber = Client.PhoneNumber;
        if (TryGetNewBirthDate(out var newBirthDate))
        {
            dbClient.BirthDate = newBirthDate;
        }
        _storage.SaveChanges();
        return RedirectToPage("Index");
    }

    private bool TryGetNewBirthDate(out DateOnly newBirthDate)
    {
        return !string.IsNullOrEmpty(NewBirthDate) && DateOnly.TryParse(NewBirthDate, out newBirthDate);
    }

    public DateOnly GetMaxBirthDate()
    {
        var maxDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-18));
        return maxDate;
    }
    public DateOnly GetMinBirthDate() => new(1900,01,01);
}