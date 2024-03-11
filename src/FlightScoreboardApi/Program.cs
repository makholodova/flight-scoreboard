using FlightScoreboardApi.Services;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FlightScoreboardContext>(options =>
	options.UseSqlServer(connectionString)); //todo:remove lazyloading

builder.Services.AddTransient<IFlightReadRepository, FlightReadRepository>();
builder.Services.AddTransient<IFlightWriteRepository, FlightWriteRepository>();
builder.Services.AddTransient<IFlightService, FlightService>();

builder.Services.AddTransient<ICityReadRepository, CityReadRepository>();
builder.Services.AddTransient<ICityWriteRepository, CityWriteRepository>();
builder.Services.AddTransient<ICityService, CityService>();

builder.Services.AddTransient<IAirplaneReadRepository, AirplaneReadRepository>();
builder.Services.AddTransient<IAirplaneWriteRepository, AirplaneWriteRepository>();
builder.Services.AddTransient<IAirplaneService, AirplaneService>();

builder.Services.AddTransient<IAirlineReadRepository, AirlineReadRepository>();
builder.Services.AddTransient<IAirlineWriteRepository, AirlineWriteRepository>();
builder.Services.AddTransient<IAirlineService, AirlineService>();

builder.Services.AddTransient<IAirlineAirplaneReadRepository, AirlineAirplaneReadRepository>();
builder.Services.AddTransient<IAirlineAirplaneWriteRepository, AirlineAirplaneWriteRepository>();
builder.Services.AddTransient<IAirlineAirplaneService, AirlineAirplaneService>();

builder.Services.AddTransient<IStatusService, StatusService>();

builder.Services.AddTransient<IPilotReadRepository, PilotReadRepository>();
builder.Services.AddTransient<IPilotWriteRepository, PilotWriteRepository>();
builder.Services.AddTransient<IPilotService, PilotService>();

builder.Services.AddTransient<IScoreboardService, ScoreboardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<FlightScoreboardContext>())
{
	context.Database.Migrate();
	//context.Database.EnsureDeleted();
	//context.Database.EnsureCreated();
}

app.Run();