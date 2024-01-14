using FlightScoreboardData.DateBase;
using FlightScoreboardData.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FlightScoreboardContext>(options =>
	options.UseLazyLoadingProxies().UseSqlServer(connectionString));

builder.Services.AddTransient<IStatusService, StatusService>();
builder.Services.AddTransient<IAirlineAirplaneService, AirlineAirplaneService>();
builder.Services.AddTransient<IAirlineService, AirlineService>();
builder.Services.AddTransient<IAirplaneService, AirplaneService>();
builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<IFlightService, FlightService>();
builder.Services.AddTransient<IPilotService, PilotService>();
builder.Services.AddTransient<IScoreboardService, ScoreboardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	"default",
	"{controller=Flight}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<FlightScoreboardContext>())
{
	context.Database.Migrate();
	//context.Database.EnsureDeleted();
	//context.Database.EnsureCreated();
}

app.Run();