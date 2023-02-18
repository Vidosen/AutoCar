namespace AutoCar.Models;

public class ContractModel
{
    public int ClientId { get; set; }
    public ushort ParkingSlotId { get; set; }
    public string CarNumber { get; set; }
    public DateOnly ContractDate { get; set; }
    public DateOnly? AccuralDate { get; set; }
    public decimal Debt { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public decimal? PaymentAmount { get; set; }
}