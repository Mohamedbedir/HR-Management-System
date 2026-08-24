using HR.Core;
using HR.Infrastructure;
using HR.Infrastructure.Contexts;
using HR.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection Sql Server
builder.Services.AddDbContext<HRAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add dependencies from other layers
builder.Services.AddInfrastructureDependencies()
                 .AddServiceDependencies()
                 .AddCoreDependencies();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;
var dbcontext = service.GetRequiredService<HRAppDbContext>();
var loggerfactory = service.GetRequiredService<ILoggerFactory>();
try
{
    await dbcontext.Database.MigrateAsync();

}catch(Exception ex)
{
    var logger = loggerfactory.CreateLogger<Program>();
    logger.LogError(ex, "An Error Occurred During Applying Migrations Database");
}

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
