using CinemaSystem.Components;
using CinemaSystem.Data;
using CinemaSystem.Services.DesignPatterns.Command;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();

// scoped for a user session
builder.Services.AddScoped<CinemaSystem.Services.CounterState>();
builder.Services.AddScoped<CinemaSystem.Services.DesignPatterns.Facade.ReservationFacade>();
builder.Services.AddScoped<CinemaSystem.Services.EventService>();
builder.Services.AddScoped<CinemaSystem.Services.CustomerService>();
builder.Services.AddScoped<CinemaSystem.Services.CinemaHallService>();
builder.Services.AddScoped<CinemaSystem.Services.SeatService>();
builder.Services.AddScoped<ReservationInvoker>();
builder.Services.AddScoped<CinemaSystem.Services.ReservationStateService>(); 
builder.Services.AddScoped<ReservationStateAsyncInvoker>();

builder.Services.AddDbContext<CinemaDbContext>(options =>
    options.UseSqlite("Data Source=cinema.db"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseSession();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
