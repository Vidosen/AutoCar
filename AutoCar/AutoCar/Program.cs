using AutoCar.Models;
using AutoCar.Storage;

void InitializeService(PostgresStorage postgresStorage)
{
    postgresStorage.Clients.Add(new ClientModel
    {
        FirstName = "Иванов",
        LastName = "Иван",
        Patronymic = "Иванович",
        BirthDate = new DateOnly(1993, 4, 20),
        PhoneNumber = "8935123456"
    });
    postgresStorage.Clients.Add(new ClientModel
    {
        FirstName = "Петров",
        LastName = "Алексей",
        Patronymic = "Петрович",
        BirthDate = new DateOnly(1986, 4, 23),
        PhoneNumber = "8241459673"
    });
    postgresStorage.SaveChanges();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var storage = new PostgresStorage();
InitializeService(storage);
builder.Services.AddSingleton<PostgresStorage, PostgresStorage>(_ => storage);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();