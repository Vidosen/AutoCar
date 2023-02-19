using System.ComponentModel.DataAnnotations;

namespace AutoCar.Models;

public class Car
{
    [Key] public string Number { get; set; }
    [Required] public string Brand { get; set; }
    [Required] public short ReleaseYear { get; set; }

}