using Common.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Payroll.Application.Interfaces.IRepositories;
using Payroll.Application.Interfaces.IServices;
using Payroll.Application.Services;
using Payroll.Infrastructure.Context;
using Payroll.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        }
    );
    option.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        }
    );
});

//db
builder.Services.AddDbContext<PayrollDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("SQL"))
);

builder.Services.AddScoped<IAllowanceService, AllowanceService>();
builder.Services.AddScoped<IAllowanceRepository, AllowanceRepository>();
builder.Services.AddAuth("OrelIT-MEp9AuVjXeGgDf3GDshnLapgpW7OM8biQ5c6moJ9");
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) //this line is commented because we need to show the swagger in production server
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

//for migration
var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<PayrollDbContext>();
db.Database.Migrate();

app.UseHttpsRedirection();
app.UseAuth();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllers();

app.Run();
