using Microsoft.EntityFrameworkCore;
using OnionSA.Database.Data;
using OnionSA.Database.Entities;
using OnionSA.Domain.Services.Interfaces;
using OnionSA.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<OnionContext>(options =>
    options.UseInMemoryDatabase("OnionDatabase"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Registra interfaces customizadas
builder.Services.AddScoped<IPedidosService>();
builder.Services.AddScoped<IImportService>();

var app = builder.Build();

// Configuração do HTTP request.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
