using Microsoft.EntityFrameworkCore;
using OnionSA.Data;
using OnionSA.Models;
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

// Apply seed data if the database is empty
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<OnionContext>();

    // Ensure the database is created
    context.Database.EnsureCreated();

    // Check if there are any products in the database
    if (!context.Produtos.Any())
    {
        // Apply seeds
        context.Produtos.AddRange(
            new ClsProduto { Id = 1, Nome = "Celular", Valor = 1000 },
            new ClsProduto { Id = 2, Nome = "Notebook", Valor = 3000 },
            new ClsProduto { Id = 3, Nome = "Televisão", Valor = 5000 }
        );

        context.SaveChanges();
    }
}

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
