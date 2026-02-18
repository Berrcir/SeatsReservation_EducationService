using Microsoft.EntityFrameworkCore;
using SeatsReservationService.Application.DataBase;
using SeatsReservationService.Application.Reservations;
using SeatsReservationService.Application.Venues;
using SeatsReservationService.Infrastructure.PostgreSql;
using SeatsReservationService.Infrastructure.PostgreSql.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IReservationServiceDbContext, ReservationServiceDbContext>(serviceProvider =>
    new ReservationServiceDbContext(builder.Configuration.GetConnectionString("ReservationServiceDb")!));

builder.Services.AddScoped<IEventsRepository, EventsRepository>();
builder.Services.AddScoped<IReservationsRepository, ReservationsRepository>();
builder.Services.AddScoped<ISeatsRepository, SeatsRepository>();
builder.Services.AddScoped<IVenuesRepository, VenuesRepository>();

builder.Services.AddSingleton<ITransactionManager, TransactionManager>();
builder.Services.AddSingleton<ITransactionScope, TransactionScope>();

builder.Services.AddScoped<CreateReservationHandler>();

builder.Services.AddScoped<CreateVenueHandler>();
builder.Services.AddScoped<UpdateVenueNameHandler>();
builder.Services.AddScoped<UpdateVenueNamesByPrefixHandler>();
builder.Services.AddScoped<UpdateVenueSeatsHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SeatsReservationService"));
}

app.MapControllers();

app.Run();