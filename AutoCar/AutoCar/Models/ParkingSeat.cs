using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCar.Models;
public class ParkingSeat
{
    [Key] public ushort Id { get; set; }
    public decimal Price { get; set; }
    public int? ContractId { get; set; }
    [ForeignKey(nameof(ContractId))] public Contract Contract { get; set; }
}