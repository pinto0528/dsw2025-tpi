using Microsoft.EntityFrameworkCore;
using Dsw2025TPI.Data;

var builder = WebApplication.CreateBuilder(args);

// ─── SERVICES ────────────────────────────────────────────────

// Configuración de conexión a SQL Server
builder.Services.AddDbContext<Dsw2025TpiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controladores
builder.Services.AddControllers();

// Swagger/OpenAPI para documentación automática
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Servicios personalizados, validadores, etc.
// builder.Services.AddScoped<IProductService, ProductService>();

// CORS si lo necesitás para frontend externo
// builder.Services.AddCors(options => { ... });

var app = builder.Build();

// ─── MIDDLEWARE ───────────────────────────────────────────────

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoint de prueba simple
app.MapGet("/api/hello", () =>
{
    return Results.Ok("¡Hola desde tu API!");
});

app.UseHttpsRedirection();
// app.UseCors("NombreDeTuPolitica");
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();
