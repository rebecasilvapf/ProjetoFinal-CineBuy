using ApiSistemaReservaIngressos.Data;
using ApiSistemaReservaIngressos.Repositories;
using ApiSistemaReservaIngressos.Services;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<DbSession>(); 
builder.Services.AddTransient<FilmeRepository>(); 
builder.Services.AddTransient<FilmeService>();
builder.Services.AddTransient<HorarioRepository>();
builder.Services.AddTransient<HorarioService>();
builder.Services.AddTransient<AssentoRepository>();
builder.Services.AddTransient<AssentoService>();
builder.Services.AddTransient<ReservaRepository>();
builder.Services.AddTransient<ReservaService>();
builder.Services.AddTransient<DetalhesReservaRepository>();
builder.Services.AddTransient<DetalhesReservaService>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
