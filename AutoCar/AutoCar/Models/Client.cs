using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AutoCar.Models;

[PrimaryKey(nameof(Id))]
public class Client
{
    [Key] public int Id { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    public string Patronymic { get; set; }
    [Required] public DateOnly BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Contract> Contracts { get; set; }
    public string GetFullName() => string.Join(' ', LastName, FirstName, Patronymic);
}