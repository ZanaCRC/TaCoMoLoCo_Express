using Microsoft.AspNetCore.Authentication.Cookies;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using TaCoMoLoCo_Express.BL;
using TaCoMoLoCo_Express.DA;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
// Accede a la cadena de conexi�n
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<NpgsqlConnection>(sp => new NpgsqlConnection(connectionString));
builder.Services.AddScoped<IAdministradorDeUsuarios, AdministradorDeUsuarios>();
builder.Services.AddScoped<IAdministradorDePedidos, AdministradorDePedidos>();
builder.Services.AddScoped<IAdministradorDePlatos, AdministradorDePlatos>();
builder.Services.AddScoped<IAdministradorDeRestaurantes, AdministradorDeRestaurantes>();
builder.Services.AddDbContext<DBContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

})
.AddCookie(options =>
{
    options.LoginPath = "/Login/InicieSesion";
    options.AccessDeniedPath = "/Login/InicieSesion";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});


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

app.UseAuthentication();

app.UseAuthorization();
app.UseSession();

//app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MenuPlatos}/{action=Index}/{id?}");

app.Run();
