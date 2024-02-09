using Microsoft.EntityFrameworkCore;
using Payroll.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//db
builder.Services.AddDbContext<PayrollDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//for migration
var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<PayrollDbContext>();
db.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();