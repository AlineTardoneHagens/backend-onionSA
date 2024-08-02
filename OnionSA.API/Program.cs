using Microsoft.EntityFrameworkCore;
using OnionSA.Database.Data;
using OnionSA.Database.Entities;
using OnionSA.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<OnionContext>(options =>
    options.UseInMemoryDatabase("OnionDatabase"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Registra interfaces customizadas
builder.Services.AddScoped<PedidosService>();
builder.Services.AddScoped<ImportService>();

// Configuração do serviço de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Setup Seed Data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<OnionContext>();
    if (!context.Produtos.Any())
    {
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

// Aplicação da política de CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
