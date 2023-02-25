using System.ComponentModel.DataAnnotations;

namespace AutoCar.Models;
public class ParkingSeat
{
    [Key] public ushort Id { get; set; }
    public decimal Price { get; set; }
    public Contract Contract { get; set; }
}