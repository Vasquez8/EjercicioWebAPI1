var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
var proveedor = new List<Proveedores>();

app.MapGet("/proveedores", () =>
{
    return proveedor; 
});

app.MapGet("/proveedores/{id}", (int id) =>
{
    var prov = proveedor.FirstOrDefault(p => p.Id == id);
    return prov;
});

app.MapPost("/proveedores", (Proveedores pPoveedores) =>
{
    proveedor.Add(pPoveedores);
    return Results.Ok();
});
app.MapPut("/proveedores/{id}", (int id, Proveedores pProveedores) =>
{
    var existingProv = proveedor.FirstOrDefault(p => p.Id == id);
    if (existingProv != null)
    {
        existingProv.Nombre = pProveedores.Nombre;
        existingProv.Apellido = pProveedores.Apellido;
        existingProv.Correo = pProveedores.Correo;
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});
app.MapDelete("/proveedores/{id}", (int id) =>
{
    var existingProv = proveedor.FirstOrDefault(p => p.Id == id);
    if (existingProv != null)
    {
        proveedor.Remove(existingProv);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();

internal class Proveedores
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Correo { get; set; }
}