using AutoCar.Models;
using AutoCar.Services;
using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoCar.Pages.Cars;
public class NewCarModel : PageModel
{
    [BindProperty] public Car Car { get; set; }
    public IEnumerable<string> ValidationMessages { get; private set; } = Enumerable.Empty<string>();
    public void OnGet()
    {
        Car = new Car
        {
            Number = ValidationService.SAMPLE_CAR_NUMBER,
            ReleaseYear = ValidationService.MAX_CAR_RELEASE_YEAR
        };
    }
    public IActionResult OnPost([FromServices] ValidationService service)
    {

        var dbConsistencyMessages = new List<string>();
        CheckStorageForExistingCarWithSameNumber(dbConsistencyMessages);
        service.ValidateCarNumber(Car.Number);
        service.ValidateCarReleaseYear(Car.ReleaseYear);
        if (dbConsistencyMessages.Count == 0 && service.PassedAllValidations)
        {
            using (var storage = new PostgresStorage())
            {
                storage.Cars.Add(Car);
                storage.SaveChanges();
            }
        }
        ValidationMessages = dbConsistencyMessages.Concat(service.GetValidationMessages());
        return service.PassedAllValidations ? RedirectToPage("Index") : Page();
    }

    private void CheckStorageForExistingCarWithSameNumber(List<string> dbConsistencyMessages)
    {
        using (var storage = new PostgresStorage())
        {
            var foundCar = storage.Cars.Find(Car.Number);
            if (foundCar != null) dbConsistencyMessages.Add($"Aвтомобиль с номером {Car.Number} уже добавлен!");
        }
    }
}