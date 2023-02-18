using Microsoft.EntityFrameworkCore;

namespace AutoCar.Models;

[PrimaryKey(nameof(CarNumber))]
public class CarModel
{
    public string CarNumber { get; set; }
    public string? Brand { get; set; } = null;
    public short ReleaseYear { get; set; }

}