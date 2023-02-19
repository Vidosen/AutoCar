using AutoCar.Models;
using Microsoft.EntityFrameworkCore;
namespace AutoCar.Storage;

public class PostgresStorage : DbContext
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<ParkingSlot> ParkingSlots => Set<ParkingSlot>();
    public PostgresStorage()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    public void Initialize()
    {
        Clients.Add(new Client
        {
            FirstName = "Иван",
            LastName = "Иванов",
            Patronymic = "Иванович",
            BirthDate = new DateOnly(1993, 4, 20),
            PhoneNumber = "8935123456"
        });
        Clients.Add(new Client
        {
            FirstName = "Алексей",
            LastName = "Петров",
            Patronymic = "Петрович",
            BirthDate = new DateOnly(1986, 4, 23),
            PhoneNumber = "8241459673"
        });
        for (ushort i = 1; i < 1000; i++)
        {
            var price = i == 174 ? 400 : i == 53 ? 100 : new Random().Next(100, 500) / 50 * 50;
            ParkingSlots.Add(new ParkingSlot() { Id = i, Price = price });
        }
        SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=demo_db;Username=postgres;Password=autocar");
        optionsBuilder.LogTo(Console.WriteLine);
    }  
}

