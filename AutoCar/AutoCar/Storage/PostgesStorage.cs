// ICVR CONFIDENTIAL
// __________________
// 
// [2016] - [2023] ICVR LLC
// All Rights Reserved.
// 
// NOTICE:  All information contained herein is, and remains
// the property of ICVR LLC and its suppliers,
// if any.  The intellectual and technical concepts contained
// herein are proprietary to ICVR LLC
// and its suppliers and may be covered by U.S. and Foreign Patents,
// patents in process, and are protected by trade secret or copyright law.
// Dissemination of this information or reproduction of this material
// is strictly forbidden unless prior written permission is obtained
// from ICVR LLC.

using AutoCar.Models;
using Microsoft.EntityFrameworkCore;
namespace AutoCar.Storage;

public class PostgresStorage : DbContext
{
    public DbSet<ClientModel> Clients => Set<ClientModel>();
    public DbSet<CarModel> Cars => Set<CarModel>();
    public DbSet<ParkingSlotModel> ParkingSlots => Set<ParkingSlotModel>();
    public PostgresStorage()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=demo_db;Username=postgres;Password=autocar");
        optionsBuilder.LogTo(Console.WriteLine);
    }  
}
