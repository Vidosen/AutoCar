using AutoCar.Models;
using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoCar.Pages.Contracts;

public class NewContractModel : PageModel
{
    [BindProperty] public Contract Contract { get; set; }
    public IEnumerable<string> ValidationMessages { get; private set; } = Enumerable.Empty<string>();
    public void OnGet(ushort seatId = 0)
    {
        if (seatId != 0) Contract = new Contract { ClientId = 1, SeatId = seatId };
    }
    public IActionResult OnPost()
    {
        var dbConsistencyMessages = new List<string>();
        bool isValid;
        using (var storage = new PostgresStorage())
        {
            CheckStorageForExistingClient(storage, dbConsistencyMessages);
            CheckStorageForExistingCar(storage, dbConsistencyMessages);
            CheckStorageForValidParkingSeat(storage, dbConsistencyMessages);
            isValid = dbConsistencyMessages.Count == 0;
            if (isValid)
            {
                Contract.ContractDate = DateOnly.FromDateTime(DateTime.Today);
                Contract.Debt = 0;
                storage.Contracts.Add(Contract);
            }

            storage.SaveChanges();
        }
        ValidationMessages = dbConsistencyMessages;
        return isValid ? RedirectToPage("Index") : Page();
    }

    private void CheckStorageForExistingClient(PostgresStorage storage, List<string> dbConsistencyMessages)
    {
        var client = storage.Clients.Find(Contract.ClientId);
        if (client == null) dbConsistencyMessages.Add($"Клиента с ID {Contract.ClientId} нет в базе!");
    }

    private void CheckStorageForExistingCar(PostgresStorage storage, List<string> dbConsistencyMessages)
    {
        var car = storage.Cars.Find(Contract.CarNumber);
        if (car == null) dbConsistencyMessages.Add($"Автомобиля с номером {Contract.CarNumber} нет в базе!");
    }
    
    private void CheckStorageForValidParkingSeat(PostgresStorage storage, List<string> dbConsistencyMessages)
    {
        var seat = storage.ParkingSeats.Find(Contract.SeatId);
        if (seat == null)
            dbConsistencyMessages.Add($"Парковочного места с номером {Contract.SeatId} нет в базе!");
        else if (seat.Contract != null)
            dbConsistencyMessages.Add($"Парковочное место с номером {Contract.SeatId} уже арендовано " +
                                      $"в рамках договора #{Contract.Seat.Contract.Id}!");
    }
}