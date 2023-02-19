using System.ComponentModel.DataAnnotations;

namespace AutoCar.Models;
public class ParkingSlot
{
    [Key] public ushort Id { get; set; }
    public decimal Price { get; set; }
    public Contract Contract { get; set; }
}