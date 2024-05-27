using Application.ImageApp;
using Application.Services.Contracts;
using Application.Services.Service;
using Data.ConexionDB;
using Data.Contracts;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(logging => {
    logging.AddProvider(new SqlLoggerProvider(builder.Configuration.GetConnectionString("ms_configurationContext") ?? throw new InvalidOperationException("Connection string 'ms_configurationContext' not found.")));
});

builder.Services.AddDbContext<Ms_configurationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ms_configurationContext") ?? throw new InvalidOperationException("Connection string 'ms_configurationContext' not found.")));

// Add services to the container.
builder.Services.AddScoped<IRepositoryImage, ImageRepository>();
builder.Services.AddScoped<IFileService, FileService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateImage.Query).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllImages.Query).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteImage.Query).Assembly));


builder.Services.AddCors(options => {
    options.AddPolicy("ReactPolicy", policy => {
        policy.WithOrigins("http://localhost:3000") // Permitir solo desde esta dirección
              .AllowAnyMethod() // Permitir cualquier método HTTP (GET, POST, etc.)
              .AllowAnyHeader() // Permitir cualquier encabezado HTTP
              .AllowCredentials(); // Permitir credenciales (si es necesario)
    });
});

var app = builder.Build();

app.Logger.LogInformation("Iniciando la aplicación");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("ReactPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
