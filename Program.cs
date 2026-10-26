using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CooperativaContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Cooperativa")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/socios", async (CooperativaContext db) =>
         await db.Socios.ToListAsync());

app.MapGet("/socios/{id}", async (int id, CooperativaContext db) =>
         await db.Socios.FindAsync(id) is Socio s ? Results.Ok(s) : Results.NotFound());
        
app.MapPost("/socios", async (Socio socio, CooperativaContext db) =>
{
    socio.FechaIngreso = DateTime.Today;
    db.Socios.Add(socio);
    await db.SaveChangesAsync();
    return Results.Created($"/socios/{socio.SocioId}", socio);
});

app.MapPut("/socios/{id}", async (int id, Socio datos, CooperativaContext db) =>
{
   var socio = await db.Socios.FindAsync(id); 
   if (socio is null) return Results.NotFound();
   socio.Nombre = datos.Nombre;
   socio.Municipio = datos.Municipio;
   await db.SaveChangesAsync();
   return Results.NoContent();
});

app.MapDelete("/socios/{id}",  async (int id, CooperativaContext db) =>
{
   var socio = await db.Socios.FindAsync(id);
   if (socio is null) return Results.NotFound();
   db.Socios.Remove(socio);
    await db.SaveChangesAsync(); 
    return Results.NoContent();
});

app.Run();

public class Socio
{
    public int SocioId {get; set;}
    public string Nombre {get; set;} = "";
    public string? Municipio {get; set;}
    public DateTime FechaIngreso {get; set;}
}

public class CooperativaContext : DbContext
    {
        public CooperativaContext(DbContextOptions<CooperativaContext> options) : base(options) {}
        public DbSet<Socio> Socios => Set<Socio>();
    }