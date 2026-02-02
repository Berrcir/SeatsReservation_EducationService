using Microsoft.EntityFrameworkCore;
using SeatsReservationService.Infrastructure.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ReservationServiceDbContext>(serviceProvider =>
    new ReservationServiceDbContext(builder.Configuration.GetConnectionString("ReservationServiceDb")!));

var app = builder.Build();

app.UseHttpsRedirection();
