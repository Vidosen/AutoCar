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

    public async Task<DataTable> ExecuteReportAsync(string queryPath, IReadOnlyDictionary<string, object> parameters = null)
    {
        using var dt = new DataTable();
        NpgsqlConnection connection = null;
        try
        {
            connection = (NpgsqlConnection)Database.GetDbConnection();
            await connection.OpenAsync();
            var reportQuery = await File.ReadAllTextAsync(queryPath);
            await using var command = new NpgsqlCommand(reportQuery, connection);
            if (parameters != null)
                foreach (var pair in parameters)
                    command.Parameters.AddWithValue(pair.Key, pair.Value);
            using var adapter = new NpgsqlDataAdapter(command);
            adapter.Fill(dt);
        }
        finally
        {
            if (connection is { State: not ConnectionState.Closed }) await connection.CloseAsync();
        }
        return dt;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=demo_db;Username=postgres;Password=autocar");
        optionsBuilder.LogTo(Console.WriteLine);
    }  
}

