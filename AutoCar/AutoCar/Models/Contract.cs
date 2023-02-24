using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCar.Models;
public class Contract
{
    [Key] public int Id { get; set; }
    [Required] public int ClientId { get; set; }
    [ForeignKey(nameof(ClientId))] public Client Client { get; set; }
    [Required] public string CarNumber { get; set; }
    [ForeignKey(nameof(CarNumber))] public Car Car { get; set; }
    public ushort? SeatId { get; set; }
    [ForeignKey(nameof(SeatId))] public ParkingSeat Seat { get; set; }
    [Required] public DateOnly ContractDate { get; set; }
    public DateOnly? AccuralDate { get; set; }
    [Required] public decimal Debt { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public decimal? PaymentAmount { get; set; }
}