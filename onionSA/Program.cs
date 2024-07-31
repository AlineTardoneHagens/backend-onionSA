using Microsoft.EntityFrameworkCore;
using OnionSA.Data;
using OnionSA.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services no container.
builder.Services.AddControllers();
builder.Services.AddDbContext<OnionContext>(options =>
    options.UseInMemoryDatabase("OnionDatabase"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Registra services customizadas
builder.Services.AddScoped<PedidosService>();
builder.Services.AddScoped<ImportService>();

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
