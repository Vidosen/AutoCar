using AutoCar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace AutoCar.Pages.Client;

public class EditModel : PageModel
{
    public ClientModel Client { get; set; }
    public IActionResult OnGet(int id)
    {
        if (id > 0)
        {
            Client = new ClientModel()
            {
                Id = id
            };
            return Page();
        }
        return NotFound();
    }
    public IActionResult OnPost()
    {
        return RedirectToPage("Index");
    }

    public string GetMaxBirthDateString()
    {
        var maxDateTime = DateTime.Today - new DateTime(18, 0, 0);
        var maxDate = DateOnly.FromDayNumber(maxDateTime.Days);
        return maxDate.ToString("'yyyy'-'MM'-'dd'");
    }
    public string GetMinBirthDateStrig() => "1900-01-01";
}