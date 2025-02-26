using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infraestructure.Context;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EcommerceDbContext>(dbContextOptions => dbContextOptions.UseSqlite(
    builder.Configuration["ConnectionStrings:DBConnectionString"]));
builder.Services.AddScoped<IMinoristaRepository, MinoristaRepository>();
builder.Services.AddScoped<IMinoristaService, MinoristaService>();
builder.Services.AddScoped<IMayoristaRepository, MayoristaRepository>();
builder.Services.AddScoped<IMayoristaService, MayoristaService>();

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
