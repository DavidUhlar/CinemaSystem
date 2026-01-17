using CinemaSystem.Components;
using CinemaSystem.Data;
using CinemaSystem.Services;
using CinemaSystem.Services.DesignPatterns.Command;
using CinemaSystem.Services.DesignPatterns.Facade;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();

// scoped for a user session
builder.Services.AddScoped<ReservationFacade>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CinemaHallService>();
builder.Services.AddScoped<CateringService>();
builder.Services.AddScoped<SeatService>();
builder.Services.AddScoped<ReservationInvoker>();
builder.Services.AddScoped<ReservationStateService>(); 
builder.Services.AddScoped<ReservationStateAsyncInvoker>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<MailService>();
builder.Services.AddScoped<ReservationService>();
builder.Services.AddHttpClient<TmdbService>();

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
