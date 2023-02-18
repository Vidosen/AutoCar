﻿using Microsoft.EntityFrameworkCore;

namespace AutoCar.Models;

[PrimaryKey(nameof(Id))]
public class ClientModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? PhoneNumber { get; set; }
}