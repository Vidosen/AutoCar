using AutoCar.Models;
using Microsoft.EntityFrameworkCore;
namespace AutoCar.Storage;

public class PostgresStorage : DbContext
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<ParkingSeat> ParkingSeats => Set<ParkingSeat>();
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
        for (ushort id = 1; id < 1000; id++)
        {
            var price = id switch
            {
                174 => 400,
                53 => 100,
                _ => new Random().Next(100, 500) / 50 * 50
            };
            ParkingSeats.Add(new ParkingSeat { Id = id, Price = price });
        }
        SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=demo_db;Username=postgres;Password=autocar");
        optionsBuilder.LogTo(Console.WriteLine);
    }  
}

