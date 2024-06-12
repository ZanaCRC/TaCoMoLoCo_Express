using Microsoft.AspNetCore.Authentication.Cookies;
using Npgsql;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TaCoMoLoCo_Express.BL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAdministradorDeUsuarios, AdministradorDeUsuarios>();
builder.Services.AddScoped<IAdministradorDePedidos, AdministradorDePedidos>();
builder.Services.AddScoped<IAdministradorDePlatos, AdministradorDePlatos>();

// Accede a la cadena de conexi�n
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<NpgsqlConnection>(sp => new NpgsqlConnection(connectionString));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

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

//app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Registrarse}/{id?}");

app.Run();
