using Introduccion_A_Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opciones =>
opciones.JsonSerializerOptions.ReferenceHandler =
ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agrego la inyeccion de dependencias para mi proyecto
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer("name=databaseContext"));

/*options.UseSqlServer() Aqui podrias agregar la cadena de conexion como paranetro 
 * pero lo mejor es usar un provedor de confirguracion 
 * 
 * (comunmente se usa el 
 * archivo de json que ya viene conb el proyecto creado)   */

//Configurar Automapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
