using System.Data;
using AutoCar.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace AutoCar.Storage;

public class PostgresStorage : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<ParkingSeat> ParkingSeats { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public PostgresStorage()
    {
        Database.EnsureCreated();
    }

    public async Task<DataTable> ExecuteReportAsync(string queryPath)
    {
        using var dt = new DataTable();
        NpgsqlConnection connection = null;
        try
        {
            connection = (NpgsqlConnection)Database.GetDbConnection();
            await connection.OpenAsync();
            var reportQuery = await File.ReadAllTextAsync(queryPath);
            using var com = new NpgsqlDataAdapter(reportQuery, connection);
            com.Fill(dt);
        }
        finally
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Closed) await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }
        return dt;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasData(new Client
            {
                Id = 1,
                FirstName = "Иван",
                LastName = "Иванов",
                Patronymic = "Иванович",
                BirthDate = new DateOnly(1993, 4, 20),
                PhoneNumber = "8935123456"
            },
            new Client
            {
                Id = 2,
                FirstName = "Алексей",
                LastName = "Петров",
                Patronymic = "Петрович",
                BirthDate = new DateOnly(1986, 4, 23),
                PhoneNumber = "8241459673"
            });
        modelBuilder.Entity<Car>()
            .HasData(new Car
            {
                Number = "А888НА174",
                Brand = "Mazda",
                ReleaseYear = 1965
            },
            new Car
            {
                Number = "O812OP74",
                Brand = "LADa",
                ReleaseYear = 1999
            });
        var seats = new ParkingSeat[1000];
        for (ushort id = 0; id < seats.Length; id++)
        {
            var price = id switch
            {
                174 => 400,
                53 => 100,
                _ => new Random().Next(100, 500) / 50 * 50
            };
            seats[id] = new ParkingSeat { Id = (ushort)(id + 1), Price = price };
        }
        modelBuilder.Entity<ParkingSeat>()
            .HasData(seats);

        modelBuilder.Entity<Contract>()
            .HasData(new Contract
        {
            Id = 1,
            ClientId = 1,
            CarNumber = "А888НА174",
            SeatId = 174,
            ContractDate = new DateOnly(2007, 4, 19),
            AccuralDate = new DateOnly(2007, 4, 19),
            PaymentAmount = 500,
            PaymentDate = new DateOnly(2007, 4, 20),
            Debt = -100
        }, new Contract
        {
            Id = 2,
            ClientId = 2,
            CarNumber = "O812OP74",
            SeatId = 53,
            ContractDate = new DateOnly(2007, 4, 20),
            AccuralDate = new DateOnly(2007, 4, 20),
            PaymentAmount = 100,
            PaymentDate = new DateOnly(2007, 4, 25),
            Debt = 0
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=demo_db;Username=postgres;Password=autocar");
        optionsBuilder.LogTo(Console.WriteLine);
    }  
}

