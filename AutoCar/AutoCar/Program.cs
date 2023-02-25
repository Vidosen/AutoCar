using AutoCar.Services;
using AutoCar.Storage;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
using(var storage = new PostgresStorage())
    storage.Database.EnsureDeleted();

builder.Services.AddRazorPages();
builder.Services.AddTransient<ValidationService, ValidationService>();
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