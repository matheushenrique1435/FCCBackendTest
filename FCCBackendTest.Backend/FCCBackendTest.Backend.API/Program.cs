using FCCBackendTest.Backend.DbContext;
using FCCBackendTest.Backend.Repository;
using FCCBackendTest.Backend.Repository.Interfaces;
using FCCBackendTest.Backend.Services;
using FCCBackendTest.Backend.Services.Interfaces;
using System.Data;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IClientesService, ClientesService>();
builder.Services.AddScoped(services => new RelationalDatabaseContext(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
