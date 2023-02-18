using Microsoft.EntityFrameworkCore;

namespace AutoCar.Models;
[PrimaryKey(nameof(Id))]
public class ParkingSlotModel
{
    public ushort Id { get; set; }
    public decimal Price { get; set; }
}