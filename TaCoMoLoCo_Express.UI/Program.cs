using Npgsql;
using TaCoMoLoCo_Express.BL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAdministradorDeUsuarios, AdministradorDeUsuarios>();

// Accede a la cadena de conexi�n
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<NpgsqlConnection>(sp => new NpgsqlConnection(connectionString));

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
    name: "default",
    pattern: "{controller=Usuarios}/{action=Registrarse}/{id?}");

app.Run();
