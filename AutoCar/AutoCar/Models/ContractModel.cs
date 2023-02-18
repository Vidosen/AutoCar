using Microsoft.EntityFrameworkCore;

namespace AutoCar.Models;
[PrimaryKey(nameof(Client), nameof(Car), nameof(ParkingSlot), nameof(ContractDate))]
public class ContractModel
{
    public ClientModel Client { get; set; }
    public ParkingSlotModel ParkingSlot { get; set; }
    public CarModel Car { get; set; }
    public DateOnly ContractDate { get; set; }
    public DateOnly? AccuralDate { get; set; }
    public decimal Debt { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public decimal? PaymentAmount { get; set; }
}